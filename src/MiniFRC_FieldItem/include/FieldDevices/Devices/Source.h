#pragma once
#include "FieldDevices/BaseFieldDevice.h"
#include <ESP32PWM.h>
#include <ESP32Servo.h>

#define pos_a 45
#define pos_b 135
#define shake 20

class FieldDevice_Source  : public BaseFieldDevice {
protected:
    bool Initialize();
    void EnabledChanged(bool enabled);
    bool Calibrate();

    private:
    double cur_angle = 0;
    void goto_angle(int angle);
    void Drop();
    Servo drop_server;

};