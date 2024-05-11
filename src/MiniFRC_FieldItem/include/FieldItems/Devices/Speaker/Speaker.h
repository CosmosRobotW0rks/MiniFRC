#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Debugger.h"
#include "FieldItems/BaseFieldItem.h"

class FieldItem_Speaker : public BaseFieldItem
{
    public:
    bool Initialize()
    {
        
        return true;
    }
};