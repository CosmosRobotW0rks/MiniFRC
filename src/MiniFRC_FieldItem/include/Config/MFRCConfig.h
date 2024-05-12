#pragma once
#include <Arduino.h>
#include "FieldDevices/BaseFieldDevice.h"



struct Config
{   
    
    char NETSSID[20];
    char NETPW[20];
    uint64_t SecurityKey;
    
    uint16_t FMSPORT;

    TeamColor teamColor1;
    DeviceType deviceType1;

    TeamColor teamColor2;
    DeviceType deviceType2;
    uint8_t FMSIP[4];

    Config(const char* netssid,const char* netpw, uint64_t securityKey, uint8_t* fmsip, uint16_t fmsport, TeamColor _teamColor1, DeviceType _deviceType1, TeamColor _teamColor2 = (TeamColor)0, DeviceType _deviceType2 = (DeviceType)0)
    {
        strcpy(NETSSID, netssid);
        strcpy(NETPW, netpw);
        this->SecurityKey = securityKey;

        memcpy(FMSIP, fmsip, 4);
        this->FMSPORT = fmsport;

        this->teamColor1 = _teamColor1;
        this->deviceType1 = _deviceType1;

        this->teamColor2 = _teamColor1;
        this->deviceType2 = _deviceType2;
    }

    Config(){}
};


namespace MFRCConfig
{

    bool Initialize();

    Config* GetConfig();


    bool SaveConfig(Config cfg);
}