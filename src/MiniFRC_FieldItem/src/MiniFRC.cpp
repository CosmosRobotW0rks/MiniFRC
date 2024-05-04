#include <Arduino.h>
#include "Config/MFRCConfig.h"
#include "Debugger.h"

namespace MiniFRC
{

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
        if(config == nullptr)
        {
            DebugError("Failed to get config, restarting..");
            ESP.restart();
            return;
        }
    }

    void Initialize()
    {
        Serial.begin(115200);

        InitConfig();
        DebugInfoF("Config loaded\nSSID: %s\nPassword: %s\nSecurity Key: %d\nFMS IP: %d.%d.%d.%d\nTeam Color: %d\nDevice Type: %d\n\n", config->SSID, config->PW, config->SecurityKey, config->FMSIP[0], config->FMSIP[1], config->FMSIP[2], config->FMSIP[3], config->TeamColor, config->DeviceType);
        
        
    }

    void Periodic()
    {
        
    }
}