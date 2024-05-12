#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Debugger.h"
#include "FieldDevices/BaseFieldDevice.h"

class FieldDevice_Speaker : public BaseFieldDevice
{
    public:
    bool Initialize()
    {
        
        return true;
    }
};