#include <Arduino.h>
#include <WiFi.h>
#include "Config/MFRCConfig.h"
#include "Debugger.h"

// FIELD Device INCLUDES
#include "FieldDevices/BaseFieldDevice.h"
#include "FieldDevices/Packets.h"

#include "FieldDevices/Devices/Speaker/Speaker.h"


Config *config;
void InitConfig();

void LoadFieldDevicesByConfig();

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
  
  LoadFieldDevicesByConfig();

  ConnectToFMS();
  AuthClients();

  StartPingTask();

  InitDevices();
}

void loop()
{
  if(Device1Exists) Device1->Periodic();
  if(Device2Exists) Device2->Periodic();
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


int pingFailCount = 0;
void StartPingTask()
{
  DebugInfo("STARTING DA PING TASK");
  xTaskCreate([](void *pvParameters)
  {
    Packet_Ping_0 pingPacket;

    while (true)
    {
      bool device1Suc = !Device1Exists ? true : Device1Client->SendPacket(0, &pingPacket, sizeof(Packet_Ping_0));
      bool device2Suc = !Device2Exists ? true : Device2Client->SendPacket(0, &pingPacket, sizeof(Packet_Ping_0));

      bool pingsuc = device1Suc && device2Suc;
      if(!pingsuc)
      {
        pingFailCount++;
        DebugWarningF("Failed to send ping packet (%d)", pingFailCount);

        if(pingFailCount == 10)
        {
          DebugError("Failed to send ping packet 10 times, restarting..");
          ESP.restart();
        }
      }
      else pingFailCount = 0;
      delay(1000);
    }
  },
  "PingTask", 4096, nullptr, 1, nullptr);
  
  DebugInfo("STARTED DA PING TASK");
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
{
  WiFi.begin(config->NETSSID, config->NETPW);
  DebugInfo("Connecting to WiFi");
  wl_status_t status = WaitForConnectionResult(20000);
  if(status == WL_CONNECTED)
    DebugInfo("Connected to WiFi");
  else
  {
    DebugErrorF("Failed to connect to WiFi (status: %d), restarting..", status);
    ESP.restart();
    return;
  }

  if(Device1Exists)
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
  client->RegisterPacket(2, sizeof(Packet_ClientIDResponse_2), (PacketCallback)[](uint8_t *data, size_t len)
  {
    Packet_ClientIDResponse_2* packet = (Packet_ClientIDResponse_2*)data;

    if (!packet->Accepted)
    {
      DebugError("Aww, Auth request rejected :(");
      ESP.restart();
      return;
    }

    authRes = packet->Accepted;
  });

  Packet_ClientID_1 authPacket;
  authPacket.teamColor = color;
  authPacket.deviceType = device;
  authPacket.SecurityKey = config->SecurityKey;


  bool authPacketSent = client->SendPacket(1, &authPacket, sizeof(Packet_ClientID_1));
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

   if(Device1Exists) Device1 = GetFieldDeviceByDeviceType(config->deviceType1);
   if(Device2Exists) Device2 = GetFieldDeviceByDeviceType(config->deviceType2);

   DebugInfo("Loaded field devices");
}

void InitDevices()
{
  if(Device1Exists)
  {
    Device1->deviceType = config->deviceType1;
    Device1->teamColor = config->teamColor1;
    Device1->Client = Device1Client;
    Device1->Initialize();
  }
  if(Device2Exists)
  {
    
    Device2->deviceType = config->deviceType2;
    Device2->teamColor = config->teamColor2;
    Device2->Client = Device2Client;
    Device2->Initialize();
  }
}