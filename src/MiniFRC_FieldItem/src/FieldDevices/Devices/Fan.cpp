#include "FieldDevices/Devices/Fan.h"
#include <esp32-hal.h>
#include <esp32-hal-ledc.h>


bool FieldDevice_Fan::Initialize()
{
    ledcSetup(0, 25000, 8);
    ledcAttachPin(27, 0);
    ledcWrite(0, 255);
    delay(1000);
    ledcWrite(0, 0);   

    write(0);
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