#include <Arduino.h>
#include <libs/LittleFS/LittleFS.h>
#include "FS.h"

struct Config
{    char* SSID;
    char* PW;
    uint64_t SecurityKey;
    
    uint8_t FMSIP[4];

    uint8_t TeamColor;
    uint8_t DeviceType;
};

namespace MFRCConfig
{
    Config* config = nullptr;

    Config* GetConfig()
    {
        if(config == nullptr)
        {
            
        }
    }
}