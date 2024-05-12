#include <Arduino.h>
#include "DataSaving.h"
#include "Config/MFRCConfig.h"
#include "FieldDevices/BaseFieldDevice.h"





namespace MFRCConfig
{
    const char* ConfigPath = "/config";

    Config* config = nullptr;

    bool Initialize()
    {

        bool res = DataSaving::Initialize();
        if(!res)
        {
            DebugError("Failed to initialize DataSaving");
        }
        else
        {
            DebugInfo("Initialized littlefs");

            uint8_t ipaddr[] = {10,134,100,230};
            Config toSave = Config("BK_School", "8K-$cH0oL!", 16777216, ipaddr, 8081, TeamColor::RED, DeviceType::Speaker);

            //bool suc = SaveConfig(toSave);
            //DebugInfoF("Save Config Suc: %d", suc ? 1 : 0);
        }
        
        return res;
    }

    Config* GetConfig()
    {
        if(config == nullptr)
        {
            if(!DataSaving::FileExists(ConfigPath))
            {
                DebugError("Config file doesn't exist");
                return nullptr;
            }

            size_t len = sizeof(Config);
            uint8_t* data = new uint8_t[len];

            if(DataSaving::ReadData(ConfigPath, data, len) != len)
            {
                DebugError("Failed to read config file");
                return nullptr;
            }



            config = (Config*)data;   

            DebugInfoF("Config loaded\nSSID: %s\nPassword: %s\nSecurity Key: %d\nFMS IP: %d.%d.%d.%d\nTeam Color1: %d\nDevice Type1: %d\nTeam Color2: %d\nDevice Type2: %d\n\n", config->NETSSID, config->NETPW, config->SecurityKey, config->FMSIP[0], config->FMSIP[1], config->FMSIP[2], config->FMSIP[3], config->teamColor1, config->deviceType1,config->teamColor2, config->deviceType2);

        }
        
        return config;

    }

    
    bool SaveConfig(Config cfg)
    {
        uint8_t data[sizeof(Config)];
        memcpy(data, &cfg, sizeof(Config));

        bool s = DataSaving::DeleteFile(ConfigPath);
        if(!s) DebugWarning("Failed to delete old config file");
        if(DataSaving::WriteData(ConfigPath, data, sizeof(Config)) != sizeof(Config))
        {
            DebugError("Failed to write config file");
            return false;
        }

        DebugInfo("Saved Config");
        return true;
    }
}