#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Debugger.h"
#include "FieldDevices/BaseFieldDevice.h"
#include "FieldModules/LaserDetector.h"

#define PIN_BTNAMP 26
#define PIN_BTNSOURCE 27

#define PIN_LEDAMP 33
#define PIN_LEDSOURCE 25

class FieldDevice_DriverStation  : public BaseFieldDevice
{
    protected:
    bool Initialize();
    void EnabledChanged(bool enabled);

    private:
    
    static void task(void* param);

    volatile static bool AmpButtonPressed;
    volatile static bool SourceButtonPressed;

    static bool AmplifyCooldown;
    static bool SourceCooldown;

    void OnReadyToAmplify();
    void OnReadyToTriggerSource();

    void OnAmpButtonPress();
    void OnSourceButtonPress();

    void OnAmplify();
    void OnSourceTrigger();

    void UpdateBlinkStatus();

    static void ToggleAmpLED(bool state);
    static void ToggleSourceLED(bool state);

    bool is_enabled = false;

    public:
};