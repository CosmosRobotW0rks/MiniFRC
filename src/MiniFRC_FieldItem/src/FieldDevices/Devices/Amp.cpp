#include "FieldDevices/Devices/Amp.h"
#include "Debugger.h"

bool FieldDevice_Amp::Initialize()
{
    ledcSetup(RAMP_FWD_CH, 20, 8);
    ledcSetup(RAMP_BWD_CH, 20, 8);

    ledcAttachPin(RAMP_FWD, RAMP_FWD_CH);
    ledcAttachPin(RAMP_BWD, RAMP_BWD_CH);

    note_detector.attach(LASER_P, LDR);
    note_detector.calibrate();

    Client->RegisterPacket(Packet_Amp_AmplificationEnded_ID, sizeof(Packet_Amp_AmplificationEnded), (PacketCallback)[](uint8_t * data, size_t len, void *args) {
        ((FieldDevice_Amp*)args)->amplify();
    }, this);

    xTaskCreate(task, "amp", 4096, this, 0, NULL);

    return true;
}

void FieldDevice_Amp::EnabledChanged(bool enabled)
{
    this->enabled = enabled;
}

bool FieldDevice_Amp::Calibrate()
{
    return note_detector.calibrate();
}

void FieldDevice_Amp::set_override(double ramp_pw)
{
    this->ramp_pw = ramp_pw;

    ovr = true;
}

void FieldDevice_Amp::disable_override()
{
    ovr = false;
}

void FieldDevice_Amp::amplify()
{
    amplified = true;
}

void FieldDevice_Amp::reg_on_note(void (*event_f)(void *), void *arg)
{
    on_note_f = event_f;
}

void FieldDevice_Amp::task(void *param)
{
    FieldDevice_Amp *this_ = (FieldDevice_Amp *)param;
    while (true)
    {
        this_->update();
        delay(0);
    }
}

void FieldDevice_Amp::update()
{
    update_note();
    update_motors();
    ESP_LOGI("amp", "Detected: %d", has_note);

    note_detector.toggle_laser(enabled);
    amplified = amplified && has_note;
}

void FieldDevice_Amp::update_note()
{
    bool detected = note_detector.detect();
    // ESP_LOGI("spk", "Detected: %d", detected);

    if (!detected)
        last_note_enter_us = 0;

    // if (detected && last_note_enter_us != 0 && esp_timer_get_time() - last_note_enter_us > 1250000)
    //     detected = false;

    if (detected && last_note_enter_us == 0)
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

    if (!had_note && has_note)
    {
        on_note();
    }

    if (had_note && !has_note)
        last_note_exit_us = esp_timer_get_time();

    had_note = has_note;
}

void FieldDevice_Amp::update_motors()
{
    double r_pwr;

    if (enabled)
        r_pwr = 1;

    if (has_note)
        r_pwr = 0;

    if (has_note && (esp_timer_get_time() - last_note_enter_us < 500000))
        r_pwr = 0.35;

    if (has_note && amplified)
        r_pwr = 1;

    if (ovr)
        r_pwr = ramp_pw;

    if (!enabled)
        r_pwr = 0;

    motor_write(RAMP_FWD_CH, RAMP_BWD_CH, r_pwr * 255.0);
}

void FieldDevice_Amp::motor_write(int f_ch, int b_ch, int16_t pwr)
{
    if (pwr > 0)
    {
        ledcWrite(f_ch, abs(pwr));
        ledcWrite(b_ch, 0);
    }
    else
    {
        ledcWrite(b_ch, abs(pwr));
        ledcWrite(f_ch, 0);
    }
}

void FieldDevice_Amp::on_note()
{
    DebugInfo("Sending score packet");
    Packet_Speaker_Score p;
    bool suc = Client->SendPacket(Packet_Amp_Score_ID, &p, sizeof(Packet_Amp_Score));
    if (!suc)
    {
        DebugError("Failed to send score packet");
    }
    else
        DebugInfo("SENT PACKET!!!!");
}
