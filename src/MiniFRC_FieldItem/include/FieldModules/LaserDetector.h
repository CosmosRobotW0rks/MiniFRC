#pragma once
#include <Arduino.h>

class LaserDetector
{
public:
    LaserDetector() {}

    void attach(uint8_t laserPin, uint8_t detectorPin)
    {
        pinMode(laserPin, OUTPUT);
        pinMode(detectorPin, INPUT);

        _laserPin = laserPin;
        _detectorPin = detectorPin;
    }

    bool calibrate()
    {
        DebugInfo("Calibrating LDR...");
        toggle_laser(true);
        delay(100);
        uint16_t on_read = get_reading(128); // 5000

        toggle_laser(false);
        delay(100);
        uint16_t off_read = get_reading(128); // 2000

        DebugInfoF("LDR Low value: %d, high value: %d\n", off_read, on_read);

        int16_t diff = on_read - off_read;
        threshold = off_read + diff * 0.25;
        DebugInfoF("LDR Diff: %d, threshold: %d\n", diff, threshold);

        thr_sign = diff > 0;

        bool detected_before = detect();

        toggle_laser(true);
        bool detected_after = detect();

        DebugInfo("LDR Done");
        if (detected_after && !(detected_before))
            return true;

        return false;
    }

    bool detect()
    {
        if (threshold == -1)
            return false;
        if (!laser_enabled)
            return false;

        uint16_t val = get_reading(128);

        // ESP_LOGI("ldr", "value: %d", val);
        return ((val <= threshold) == thr_sign);
    }

    void toggle_laser(bool state)
    {
        laser_enabled = state;
        digitalWrite(_laserPin, state);
    }

private:
    uint16_t get_reading(uint8_t samples)
    {
        double reading = 0;
        for (uint8_t i = 0; i < samples; i++)
        {
            reading += analogRead(_detectorPin);
        }

        return reading / ((double)samples);
    }

    uint16_t threshold = -1;
    bool thr_sign = true;

    bool laser_enabled = false;

    uint8_t _laserPin;
    uint8_t _detectorPin;
};