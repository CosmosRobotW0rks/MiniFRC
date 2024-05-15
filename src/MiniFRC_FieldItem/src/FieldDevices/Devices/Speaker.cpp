#include "FieldDevices/Devices/Speaker/Speaker.h"
#include "Debugger.h"


bool FieldDevice_Speaker::Initialize()
{
    ledcSetup(CONVEYOR_FWD_CH, 20, 8);
    ledcSetup(CONVEYOR_BWD_CH, 20, 8);

    ledcSetup(RAMP_FWD_CH, 20, 8);
    ledcSetup(RAMP_BWD_CH, 20, 8);

    ledcSetup(INTAKE_FWD_CH, 20, 8);

    ledcAttachPin(CONVEYOR_FWD, CONVEYOR_FWD_CH);
    ledcAttachPin(CONVEYOR_BWD, CONVEYOR_BWD_CH);
    ledcAttachPin(RAMP_FWD, RAMP_FWD_CH);
    ledcAttachPin(RAMP_BWD, RAMP_BWD_CH);
    ledcAttachPin(INTAKE_FWD, INTAKE_FWD_CH);


    note_detector.attach(LASER_P, LDR);
    bool noteDetectorSuc = note_detector.calibrate();

    xTaskCreate(task, "spkr", 4096, this, 0, NULL);

    DebugInfo("INITIALIZED SPEAKER");
    return true;
}

void FieldDevice_Speaker::EnabledChanged(bool enabled)
{
    DebugInfoF("%s SPEAKER", enabled ? "ENABLED" : "DISABLED");
    this->enabled = enabled;
}

bool FieldDevice_Speaker::Calibrate()
{
    return note_detector.calibrate();
}

void FieldDevice_Speaker::set_override(double intake_pw, double conveyor_pw, double ramp_pw)
{
    this->intake_pw = intake_pw;
    this->conveyor_pw = conveyor_pw;
    this->ramp_pw = ramp_pw;

    ovr = true;
}

void FieldDevice_Speaker::disable_override()
{
    ovr = false;
}

void FieldDevice_Speaker::task(void* param)
{
    DebugInfo("Started speaker task");
    FieldDevice_Speaker* this_ = (FieldDevice_Speaker*)param;
    while (true) {
        this_->update();
        delay(0);
    }
}

void FieldDevice_Speaker::update()
{
    update_note();
    update_motors();

    note_detector.toggle_laser(enabled);
}

void FieldDevice_Speaker::update_note()
{
    bool detected = note_detector.detect();
    //ESP_LOGI("spk", "Detected: %d", detected);

    if (!detected)
        last_note_enter_us = 0;

    if (detected && last_note_enter_us != 0 && esp_timer_get_time() - last_note_enter_us > 1250000)
        detected = false;

    if(detected && last_note_enter_us == 0)
        last_note_enter_us = esp_timer_get_time();

    if (detected && last_note_enter_us != 0 && esp_timer_get_time() - last_note_enter_us < 150000)
        detected = false;

    if (detected && last_note_exit_us != 0 && esp_timer_get_time() - last_note_exit_us < 5000)
    {
        detected = false;
        last_note_exit_us = esp_timer_get_time();
    }

    if (detected)
        last_note_us = esp_timer_get_time();

    has_note = last_note_us != 0 && esp_timer_get_time() - last_note_us < 100000;

    if (!had_note && has_note) {
        on_note();
    }

    if (had_note && !has_note)
        last_note_exit_us = esp_timer_get_time();

    had_note = has_note;
}

void FieldDevice_Speaker::update_motors()
{
    double i_pwr, c_pwr, r_pwr;

    if (enabled) {
        i_pwr = 1;
        c_pwr = 1;
        r_pwr = 1;
    }

    if (has_note) {
        c_pwr = -0.1;
        r_pwr = 1;
        i_pwr = 0.05;
    }

    if (has_note && (esp_timer_get_time() - last_note_enter_us < 500000)) {
        c_pwr = -0.5;
    }
    
    if (!enabled) {
        i_pwr = 0;
        c_pwr = 0;
        r_pwr = 0;
    }

    if (ovr) {
        i_pwr = intake_pw;
        c_pwr = conveyor_pw;
        r_pwr = ramp_pw;
    }

    motor_write(INTAKE_FWD_CH, 0, i_pwr * 255.0);
    motor_write(CONVEYOR_FWD_CH, CONVEYOR_BWD_CH, c_pwr * 255.0);
    motor_write(RAMP_FWD_CH, RAMP_BWD_CH, r_pwr * 255.0);
}

void FieldDevice_Speaker::motor_write(int f_ch, int b_ch, int16_t pwr)
{
    if (pwr > 0) {
        ledcWrite(f_ch, abs(pwr));
        ledcWrite(b_ch, 0);
    }
    else {
        ledcWrite(b_ch, abs(pwr));
        ledcWrite(f_ch, 0);
    }
}

void FieldDevice_Speaker::on_note()
{
    DebugInfo("Sending score packet");
    Packet_Speaker_Score p;
    bool suc = Client->SendPacket(Packet_Speaker_Score_ID, &p, sizeof(Packet_Speaker_Score));
    if (!suc)
    {
        DebugError("Failed to send score packet");
    }
    else DebugInfo("SENT PACKET!!!!");
    
}
