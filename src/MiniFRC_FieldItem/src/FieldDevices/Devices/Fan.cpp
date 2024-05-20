#include "FieldDevices/Devices/Fan.h"
#include <esp32-hal.h>
#include <esp32-hal-ledc.h>


bool FieldDevice_Fan::Initialize()
{
    pinMode(electricityPin, OUTPUT);
    ledcSetup(0, 25000, 8);
    ledcAttachPin(27, 0);
    ledcWrite(0, 255);
    delay(1000);
    ledcWrite(0, 0);   

    write(0);

    Client->RegisterPacket(Packet_Fan_ToggleElectricity_ID, sizeof(Packet_Fan_ToggleElectricity), [](uint8_t* data, size_t len, void* args){
        Packet_Fan_ToggleElectricity* packet = (Packet_Fan_ToggleElectricity*)data;
        
        digitalWrite(electricityPin, !!packet->state);
    }, this);

    return true;
}

void FieldDevice_Fan::EnabledChanged(bool enabled)
{
    write(enabled ? 1 : 0);
}


void FieldDevice_Fan::write(double val)
{
    ledcWrite(0, val * 256.0);
}

bool FieldDevice_Fan::Calibrate()
{
    return true;
}