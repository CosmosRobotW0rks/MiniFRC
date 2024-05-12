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
    
    void Periodic()
    {
        Packet_Speaker_Score_3 p;

        Client->SendPacket(3, &p, sizeof(Packet_Speaker_Score_3));

        delay(2000);
    }
};