#include <Arduino.h>
#include <WiFi.h>
#include "Config/MFRCConfig.h"
#include "Debugger.h"
#include <ESPAsyncWebServer.h>
#include <ElegantOTA.h>
#include <AsyncTCP.h>

// FIELD Device INCLUDES
#include "FieldDevices/BaseFieldDevice.h"
#include "FieldDevices/Packets.h"

#include "FieldDevices/Devices/Speaker.h"
#include "FieldDevices/Devices/Amp.h"
#include "FieldDevices/Devices/Fan.h"
#include "FieldDevices/Devices/DriverStation.h"
#include "FieldDevices/Devices/Source.h"

Config *config;
void InitConfig();

void LoadFieldDevicesByConfig();

void ConnectToWiFi();
void InitOTA();
void ConnectToFMS();
void AuthClients();
void StartPingTask();
void InitDevices();


PacketClient* Device1Client = nullptr;
PacketClient* Device2Client = nullptr;

BaseFieldDevice* Device1 = nullptr;
BaseFieldDevice* Device2 = nullptr;

bool Device1Exists = false;
bool Device2Exists = false;


BaseFieldDevice* GetFieldDeviceByDeviceType(DeviceType t)
{
  switch (t)
  {
    case DeviceType::Speaker: return new FieldDevice_Speaker();
    case DeviceType::Amp: return new FieldDevice_Amp();
    case DeviceType::Fan: return new FieldDevice_Fan();
    case DeviceType::DriverStation: return new FieldDevice_DriverStation();
    case DeviceType::Source: return new FieldDevice_Source();
  
  default:
  DebugWarningF("Unknown device type %d", t);
    break;
  }

  return nullptr;
}



void setup()
{

  Serial.begin(115200);

  InitConfig();
  

  DebugInfoF("MAIN CONFIG IP: %d.%d.%d.%d\n", config->FMSIP[0], config->FMSIP[1], config->FMSIP[2], config->FMSIP[3]);
  DebugInfoF("MAIN CONFIG PORT: %d\n", config->FMSPORT);
  DebugInfoF("MAIN CONFIG TEAM COLORS: %d, %d\n", config->teamColor1, config->teamColor2);
  DebugInfoF("MAIN CONFIG DEVICE IDS: %d, %d\n\n", config->deviceType1, config->deviceType2);

  ConnectToWiFi();
  InitOTA();
  
  LoadFieldDevicesByConfig();

  ConnectToFMS();
  AuthClients();

  StartPingTask();

  InitDevices();

  DebugInfoF("D1: %d / D2: %d", (int)Device1, (int)Device2);
}

void loop()
{
  ElegantOTA.loop();
  delay(10);
}


void InitConfig()
{
  bool initSuc = MFRCConfig::Initialize();
  if (!initSuc)
  {
    DebugError("Failed to initialize MFRCConfig, restarting..");
    ESP.restart();
    return;
  }
  config = MFRCConfig::GetConfig();
  if (config == nullptr)
  {
    DebugError("Failed to get config, restarting..");
    ESP.restart();
    return;
  }
}

AsyncWebServer otaServer(80);
void InitOTA()
{
  otaServer.on("/", HTTP_GET, [](AsyncWebServerRequest *request) {
    request->send(200, "text/plain", String(config->deviceType1) + "/" + String(config->teamColor1) + "/" + String(config->deviceType2) + "/" + String(config->teamColor2));
  });
  ElegantOTA.setAutoReboot(true);
  ElegantOTA.begin(&otaServer, "admin", String(config->SecurityKey).c_str());
  // ElegantOTA callbacks
  //ElegantOTA.onStart(onOTAStart);
  //ElegantOTA.onProgress(onOTAProgress);
  //ElegantOTA.onEnd(onOTAEnd);

  otaServer.begin();
  DebugInfo("OTA Started");
}

wl_status_t WaitForConnectionResult(int timeoutMS)
{
  ulong start = millis();

  while(WiFi.status() != WL_CONNECTED && millis() - start < timeoutMS)
  {
    delay(100);
  }

  return WiFi.status();
}

void ConnectToWiFi()
{
  WiFi.begin(config->NETSSID, config->NETPW);
  DebugInfo("Connecting to WiFi");
  wl_status_t status = WaitForConnectionResult(20000);
  if(status == WL_CONNECTED)
    DebugInfoF("Connected to WiFi (IP: %s) (MAC: %s)\n", WiFi.localIP().toString().c_str(), WiFi.macAddress().c_str());
  else
  {
    DebugErrorF("Failed to connect to WiFi (status: %d), restarting..", status);
    ESP.restart();
    return;
  }
}

int pingFailCount = 0;
void StartPingTask()
{
  DebugInfo("STARTING DA PING TASK");
  xTaskCreate([](void *pvParameters)
  {
    Packet_Ping pingPacket;

    while (true)
    {
      bool device1Suc = !Device1Exists ? true : Device1Client->SendPacket(Packet_Ping_ID, &pingPacket, sizeof(Packet_Ping));
      bool device2Suc = !Device2Exists ? true : Device2Client->SendPacket(Packet_Ping_ID, &pingPacket, sizeof(Packet_Ping));

      bool pingsuc = device1Suc && device2Suc;
      if(!pingsuc)
      {
        pingFailCount++;
        DebugWarningF("Failed to send ping packet (%d)", pingFailCount);

        if(pingFailCount == 10)
        {
          DebugError("Failed to send ping packet 10 times, restarting..");
          if(Device1Exists) Device1->EnabledChanged(false);
          if(Device2Exists) Device2->EnabledChanged(false);
          delay(100);
          ESP.restart();
        }
      }
      else pingFailCount = 0;
      delay(100);
    }
  },
  "PingTask", 4096, nullptr, 1, nullptr);
  
  DebugInfo("STARTED DA PING TASK");
}

void ConnectToFMSSingle(PacketClient* cli)
{
  DebugInfo("Connecting to FMS");

  bool cliconnected = cli->Connect(IPAddress(config->FMSIP), config->FMSPORT, 5000);

  if(cliconnected)
    DebugInfo("Connected to FMS");
  else
  {
    DebugError("Failed to connect to FMS, restarting..");
    ESP.restart();
  }
}

void ConnectToFMS()
{ if(Device1Exists)
  {
    Device1Client = new PacketClient();
    ConnectToFMSSingle(Device1Client);
  }

  
  if(Device2Exists)
  {
    Device2Client = new PacketClient();
    ConnectToFMSSingle(Device2Client);
  }
}

int8_t authRes = -1;
void AuthSingleClient(PacketClient* client, TeamColor color, DeviceType device)
{
  client->RegisterPacket(Packet_ClientIDResponse_ID, sizeof(Packet_ClientIDResponse), (PacketCallback)[](uint8_t *data, size_t len, void* args)
  {
    Packet_ClientIDResponse* packet = (Packet_ClientIDResponse*)data;

    if (!packet->Accepted)
    {
      DebugError("Aww, Auth request rejected :(");
      ESP.restart();
      return;
    }

    authRes = packet->Accepted;
  });

  Packet_ClientID authPacket;
  authPacket.teamColor = color;
  authPacket.deviceType = device;
  authPacket.SecurityKey = config->SecurityKey;

  IPAddress ip = WiFi.localIP();

  authPacket.IPAddr[0] = ip[0];
  authPacket.IPAddr[1] = ip[1];
  authPacket.IPAddr[2] = ip[2];
  authPacket.IPAddr[3] = ip[3];


  bool authPacketSent = client->SendPacket(Packet_ClientID_ID, &authPacket, sizeof(Packet_ClientID));
  if (!authPacketSent)
  {
    DebugError("Failed to send auth packet, restarting..");
    ESP.restart();
  }
  DebugInfo("Authenticating with FMS");


  const uint16_t TimeoutMS = 20000;
  ulong start = millis();
  while (authRes == -1 && millis() - start < TimeoutMS)
  {
    delay(10);
  }

  if (authRes == -1)
  {
    DebugError("Failed to authenticate with FMS (No Response), restarting..");
    ESP.restart();
  }
  else if(authRes == 0)
  {
    DebugError("Aww, Auth request rejected :(");
    ESP.restart();
  }
  else
  {
    DebugInfo("Authenticated with FMS");
  }
}

void AuthClients()
{
  if(Device1Exists)
  AuthSingleClient(Device1Client, config->teamColor1, config->deviceType1);

  if(Device2Exists)
  AuthSingleClient(Device2Client, config->teamColor2, config->deviceType2);
}


void LoadFieldDevicesByConfig()
{
   Device1Exists = config->deviceType1 != DeviceType::NONE;
   Device2Exists = config->deviceType2 != DeviceType::NONE;

   DebugInfoF("Devices: %d, %d", Device1Exists, Device2Exists);

   if(Device1Exists) Device1 = GetFieldDeviceByDeviceType(config->deviceType1);
   if(Device2Exists) Device2 = GetFieldDeviceByDeviceType(config->deviceType2);

   DebugInfo("Loaded field devices");
}

void InitDevices()
{
  if(Device1Exists)
  {
    DebugInfo("Initing Device 1");
    Device1->deviceType = config->deviceType1;
    Device1->teamColor = config->teamColor1;
    Device1->Client = Device1Client;
    Device1->Init();
  }
  if(Device2Exists)
  {
    DebugInfo("Initing Device 2");
    Device2->deviceType = config->deviceType2;
    Device2->teamColor = config->teamColor2;
    Device2->Client = Device2Client;
    Device2->Init();
  }
}