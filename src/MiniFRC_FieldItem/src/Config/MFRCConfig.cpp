#include <Arduino.h>
#include <ArduinoJson.h>
#include "DataSaving.h"
#include "Config/MFRCConfig.h"
#include "FieldDevices/BaseFieldDevice.h"

namespace MFRCConfig
{
    const char *ConfigPath = "/config";

    Config *config = nullptr;

    bool Initialize()
    {

        bool res = DataSaving::Initialize();
        if (!res)
        {
            DebugError("Failed to initialize DataSaving");
        }
        else
        {
            DebugInfo("Initialized littlefs");
            SaveConfig();
        }

        return res;
    }

    Config *GetConfig()
    {
        if (config != nullptr)
            return config;

        if (!DataSaving::FileExists(ConfigPath))
        {
            DebugError("Config file doesn't exist");
            return nullptr;
        }

        char data[300];

        int readRes = DataSaving::ReadData(ConfigPath, (uint8_t*)data, 300);
        if(readRes == -1)
        {
            DebugError("Failed to read config file");
            return nullptr;
        }

        JsonDocument doc;
        DeserializationError err = deserializeJson(doc, (char*)data);
        if(err)
        {
            DebugErrorF("Failed to deserialize config file (err: %s)", err.c_str());
            return nullptr;
        }


        config = new Config();

        const char* netssid = doc["NETSSID"];
        const char* netpw = doc["NETPW"];
        uint64_t securityKey = doc["SecurityKey"];

        uint8_t fmsip0 = doc["FMSIP"][0];
        uint8_t fmsip1 = doc["FMSIP"][1];
        uint8_t fmsip2 = doc["FMSIP"][2];
        uint8_t fmsip3 = doc["FMSIP"][3];


        uint16_t fmsport = doc["FMSPort"];

        uint8_t teamColor1 = doc["teamColor1"];
        uint8_t deviceType1 = doc["deviceType1"];

        uint8_t teamColor2 = doc["teamColor2"];
        uint8_t deviceType2 = doc["deviceType2"];

        DebugInfoF("JSON IP: %d.%d.%d.%d\n", fmsip0, fmsip1, fmsip2, fmsip3);
        DebugInfoF("JSON PORT: %d\n", fmsport);
        DebugInfoF("JSON TEAM COLORS: %d, %d\n", teamColor1, teamColor2);
        DebugInfoF("JSON DEVICE IDS: %d, %d\n\n", deviceType1, deviceType2);



        strcpy(config->NETSSID, netssid);
        strcpy(config->NETPW, netpw);
        config->SecurityKey = securityKey;

        config->FMSPORT = fmsport;
        config->FMSIP[0] = fmsip0;
        config->FMSIP[1] = fmsip1;
        config->FMSIP[2] = fmsip2;
        config->FMSIP[3] = fmsip3;

        config->teamColor1 = (TeamColor)teamColor1;
        config->deviceType1 = (DeviceType)deviceType1;

        config->teamColor2 = (TeamColor)teamColor2;
        config->deviceType2 = (DeviceType)deviceType2;

        DebugInfoF("CONFIG IP: %d.%d.%d.%d\n", config->FMSIP[0], config->FMSIP[1], config->FMSIP[2], config->FMSIP[3]);
        DebugInfoF("CONFIG PORT: %d\n", config->FMSPORT);
        DebugInfoF("CONFIG TEAM COLORS: %d, %d\n", config->teamColor1, config->teamColor2);
        DebugInfoF("CONFIG DEVICE IDS: %d, %d\n\n", config->deviceType1, config->deviceType2);

        return config;
    }

    bool SaveConfig()
    {
        DeviceType deviceType1 = DeviceType::DriverStation;
        TeamColor teamColor1 = TeamColor::RED;

        // TODO: SERIALIZE OR FIND A WORKAROUND

        char* data = "{\"NETSSID\":\"BK_School\",\"NETPW\":\"8K-$cH0oL!\",\"SecurityKey\":16777216,\"FMSIP\":[10,134,100,230],\"FMSPort\":8081,\"teamColor1\":2,\"deviceType1\":5,\"teamColor2\":0,\"deviceType2\":0}";
        bool s = DataSaving::DeleteFile(ConfigPath);
        if (!s)
            DebugWarning("Failed to delete old config file");
        if (DataSaving::WriteData(ConfigPath, (uint8_t*)data, strlen(data)) != strlen(data))
        {
            DebugError("Failed to write config file");
            return false;
        }

        DebugInfo("Saved Config");
        return true;
        
    }
}