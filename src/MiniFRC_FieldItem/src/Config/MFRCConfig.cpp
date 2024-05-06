#include <Arduino.h>
#include "DataSaving.h"
#include "Config/MFRCConfig.h"
#include "FieldItems/FieldItemID.h"





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
        else DebugInfo("Initialized littlefs");
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
            uint8_t data[len];

            if(DataSaving::ReadData(ConfigPath, data, len) != len)
            {
                DebugError("Failed to read config file");
                return nullptr;
            }

            Config cfg;

            memcpy(&cfg, data, len);

            config = new Config(cfg);    
        }
        
        return config;

    }


    bool SaveConfig(Config cfg)
    {
        uint8_t data[sizeof(Config)];
        memcpy(data, &cfg, sizeof(Config));

        if(DataSaving::WriteData(ConfigPath, data, sizeof(Config)) != sizeof(Config))
        {
            DebugError("Failed to write config file");
            return false;
        }

        DebugInfo("Saved Config");
        return true;
    }
}