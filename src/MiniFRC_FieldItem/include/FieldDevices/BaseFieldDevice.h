#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Packets.h"
#include "Debugger.h"

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
    
    virtual void EnabledChanged(bool enabled){};

    bool Init()
    {
        Client->RegisterPacket(Packet_ClientToggleEnabled_ID, sizeof(Packet_ClientToggleEnabled), (PacketCallback)[](uint8_t *data, size_t len, void* args)
        {
            Packet_ClientToggleEnabled* packet = (Packet_ClientToggleEnabled*)data;
            BaseFieldDevice* device = (BaseFieldDevice*)args;
            DebugInfoF("Receigle enabledd ppacket: %d\n", packet->State);
            device->EnabledChanged(packet->State);
        }, this);

        //return true;
        return Initialize();
    }




protected:
    virtual bool Initialize() = 0;

    virtual bool Calibrate() {return true; }
};