#include <Arduino.h>
#include <WiFi.h>
#include "Config/MFRCConfig.h"
#include "Debugger.h"

// FIELD ITEM INCLUDES
#include "FieldItems/FieldItemID.h"
#include "FieldItems/Packets.h"

#include "FieldItems/Devices/Speaker/Speaker.h"

Config *config;
void InitConfig();

FieldItemInitialize fieldItemInit = nullptr;
FieldItemPeriodic fieldItemPeriodic = nullptr;
void LoadFieldItemByConfig();

void ConnectToFMS();
void AuthClient();
void StartPingTask();

void setup()
{
  Serial.begin(115200);

  InitConfig();
  DebugInfoF("Config loaded\nSSID: %s\nPassword: %s\nSecurity Key: %d\nFMS IP: %d.%d.%d.%d\nTeam Color: %d\nDevice Type: %d\n\n", config->SSID, config->PW, config->SecurityKey, config->FMSIP[0], config->FMSIP[1], config->FMSIP[2], config->FMSIP[3], config->teamColor, config->deviceType);

  LoadFieldItemByConfig();
  DebugInfo("Loaded the field item");

  ConnectToFMS();
  AuthClient();

  StartPingTask();

  bool initres = fieldItemInit();
  if (!initres)
  {
    DebugError("Failed to initialize field item, restarting..");
    ESP.restart();
    return;
  }
}

void loop()
{
  fieldItemPeriodic();
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

void StartPingTask()
{
  xTaskCreate([](void *pvParameters)
  {
    Packet_Ping_0 pingPacket;

    while (true)
    {
      bool pingsuc = PacketClient::SendPacket(0, &pingPacket, sizeof(Packet_Ping_0));
      if(!pingsuc) DebugWarning("Failed to send ping packet");
      delay(2000);
    }
  },
  "PingTask", 4096, nullptr, 1, nullptr);
}

void ConnectToFMS()
{
  WiFi.begin(config->SSID, config->PW);
  DebugInfo("Connecting to WiFi");
  wl_status_t status = (wl_status_t)WiFi.waitForConnectResult(20000);
  if(status == WL_CONNECTED)
    DebugInfo("Connected to WiFi");
  else
  {
    DebugErrorF("Failed to connect to WiFi (status: %d), restarting..", status);
    ESP.restart();
    return;
  }

  DebugInfo("Connecting to FMS");
  bool cliconnected = PacketClient::Connect(IPAddress(config->FMSIP), config->FMSPORT, 20000);
  if(cliconnected)
    DebugInfo("Connected to FMS");
  else
  {
    DebugError("Failed to connect to FMS, restarting..");
    ESP.restart();
  }
   
}

int8_t authRes = -1;
void AuthClient()
{

  PacketClient::RegisterPacket(2, sizeof(Packet_ClientIDResponse_2), (PacketClient::PacketCallback)[](uint8_t *data, size_t len)
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
  authPacket.teamColor = config->teamColor;
  authPacket.deviceType = config->deviceType;
  authPacket.SecurityKey = config->SecurityKey;


  bool authPacketSent = PacketClient::SendPacket(1, &authPacket, sizeof(Packet_ClientID_1));
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


void LoadFieldItemByConfig()
{
  switch (config->deviceType)
  {
  case DeviceType::Speaker:
    fieldItemInit = FieldItem_Speaker::Initialize;
    fieldItemPeriodic = FieldItem_Speaker::Periodic;
    break;

  default:
    DebugError("Invalid device type, restarting..");
    ESP.restart();
    break;
  }

  if (fieldItemInit == nullptr || fieldItemPeriodic == nullptr)
  {
    DebugError("Failed to load field item, restarting..");
    ESP.restart();
    return;
  }
}