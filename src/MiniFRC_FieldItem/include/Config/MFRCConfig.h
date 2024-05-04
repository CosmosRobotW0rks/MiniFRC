#pragma once
#include <Arduino.h>



struct Config
{   
    char SSID[20];
    char PW[20];
    uint64_t SecurityKey;
    
    uint8_t FMSIP[4];

    uint8_t TeamColor;
    uint8_t DeviceType;
};


namespace MFRCConfig
{


    bool Initialize();

    Config* GetConfig();


    bool SaveConfig(Config cfg);
}