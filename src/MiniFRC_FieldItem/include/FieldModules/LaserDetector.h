#pragma once
#include <Arduino.h>

class LaserDetector
{
private:
    uint8_t _laserPin;
    uint8_t _detectorPin;

    uint16_t _threshold = -1;

    const float minDiffPercent = .25f;

    void ToggleLaser(bool state)
    {
        digitalWrite(_laserPin, state);
    }

    uint16_t GetReading(uint8_t samples = 1)
    {
        uint64_t reading = 0;
        for (uint8_t i = 0; i < samples; i++)
        {
            reading += analogRead(_detectorPin);
        }
        
        return reading / samples;
    }

public:
    LaserDetector(uint8_t laserPin, uint8_t detectorPin)
    {
        _laserPin = laserPin;
        _detectorPin = detectorPin;
        pinMode(laserPin, OUTPUT);
    }

    ~LaserDetector();

    bool Calibrate()
    {
        ToggleLaser(true);
        uint16_t resOn = GetReading(5); // 5000
        
        ToggleLaser(false);
        uint16_t resOff = GetReading(5); // 2000

        if(resOff > resOn) return false;
        
        float offPercent = ((float)resOff / (float)resOn); // 0,4

        // 0,4 < 0,25
        if(offPercent < minDiffPercent) return false;

        _threshold = resOn - (resOn * offPercent);

        return true;
    }

    bool Detect()
    {
        if(_threshold == -1) return false;
        
        return GetReading(2) >= _threshold;
    }
};