#pragma once
#include <Arduino.h>



typedef bool (*FieldItemInitialize)();
typedef void (*FieldItemPeriodic)();

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




class BaseFieldItem
{
private:
     
public:
    TeamColor teamColor;
    DeviceType deviceType;
    BaseFieldItem(){}
    ~BaseFieldItem(){}

    void SetDeviceInfo(TeamColor teamColor, DeviceType deviceType)
    {
    this->teamColor = teamColor;
    this->deviceType = deviceType;
    }

    virtual bool Initialize() = 0;
};
