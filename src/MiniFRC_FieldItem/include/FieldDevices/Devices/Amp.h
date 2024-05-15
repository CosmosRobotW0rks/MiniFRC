#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Debugger.h"
#include "FieldDevices/BaseFieldDevice.h"
#include "FieldModules/LaserDetector.h"

#define RAMP_FWD 27
#define RAMP_BWD 26

#define LASER_P 13
#define LDR 36

#define RAMP_FWD_CH 2
#define RAMP_BWD_CH 3

class FieldDevice_Amp  : public BaseFieldDevice {
protected:
    bool Initialize();
    void EnabledChanged(bool enabled);
    bool Calibrate();
public:
    void set_override(double ramp_pw);
    void disable_override();
    void amplify();

    void reg_on_note(void(*event_f)(void*), void* arg);
private:
    static void task(void* param);
    void update();

    void on_note();

    void update_note();
    void update_motors();

    void motor_write(int f_ch, int b_ch, int16_t pwr);

    LaserDetector note_detector;

    bool enabled = false;

    double ramp_pw = 0;
    bool ovr = false;

    bool has_note = false;
    bool had_note = false;

    bool amplified = false;

    uint64_t last_note_us;
    uint64_t last_note_enter_us;
    uint64_t last_note_exit_us;

    void(*on_note_f)(void*) = nullptr;
    void* on_note_f_arg;
};