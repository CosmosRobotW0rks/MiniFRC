#pragma once
#include <Arduino.h>
#include "FieldItems/FieldItemID.h"



struct Config
{   
    char SSID[20];
    char PW[20];
    uint64_t SecurityKey;
    
    uint8_t FMSIP[4];
    uint16_t FMSPORT;

    TeamColor teamColor;
    DeviceType deviceType;
};


namespace MFRCConfig
{


    bool Initialize();

    Config* GetConfig();


    bool SaveConfig(Config cfg);
}