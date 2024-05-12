#pragma once
#include <Arduino.h>
#include "PacketClient.h"


typedef bool (*FieldDeviceInitialize)();
typedef void (*FieldDevicePeriodic)();

enum DeviceType : uint8_t
{
    NONE = 0,
    Speaker = 1
};

enum TeamColor : uint8_t
{
    RED = 0,
    BLUE = 1
};




class BaseFieldDevice
{
private:
     
public:
    TeamColor teamColor;
    DeviceType deviceType;
    PacketClient* Client;

    BaseFieldDevice(){}
    ~BaseFieldDevice(){}

    virtual bool Initialize() = 0;
    virtual void Periodic() = 0;
};
