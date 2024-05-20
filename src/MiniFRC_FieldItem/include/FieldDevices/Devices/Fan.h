#pragma once
#include "FieldDevices/BaseFieldDevice.h"
#define electricityPin 13

class FieldDevice_Fan  : public BaseFieldDevice {
protected:
    bool Initialize();
    void EnabledChanged(bool enabled);
    bool Calibrate();
public:
    void write(double val);

};