#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Packets.h"
#include "Debugger.h"

enum DeviceType : uint8_t
{
    NONE = 0,
    Speaker,
    Amp,
    Source,
    Trap,
    DriverStation,
    Stage,
    Fan
};

enum TeamColor : uint8_t
{
    TCNONE = 0,
    RED = 1,
    BLUE = 2
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
    
    virtual void EnabledChanged(bool enabled);

    bool Init()
    {
        Client->RegisterPacket(Packet_ClientToggleEnabled_ID, sizeof(Packet_ClientToggleEnabled), (PacketCallback)[](uint8_t *data, size_t len, void* args)
        {
            Packet_ClientToggleEnabled* packet = (Packet_ClientToggleEnabled*)data;
            BaseFieldDevice* device = (BaseFieldDevice*)args;
            DebugInfoF("Receigle enabledd ppacket: %d\n", packet->State);
            device->EnabledChanged(packet->State);
        }, this);

        bool initStatus = Initialize();

        Packet_ClientInitializationStatus packet;
        packet.Success = initStatus;

        if(!Client->SendPacket(Packet_ClientInitializationStatus_ID, (uint8_t*)&initStatus, sizeof(Packet_ClientInitializationStatus)))
        {
            DebugError("Failed to send initialization status packet");
        }

        //return true;
        return initStatus;
    }




protected:
    virtual bool Initialize() = 0;

    virtual bool Calibrate() {return true; }
};