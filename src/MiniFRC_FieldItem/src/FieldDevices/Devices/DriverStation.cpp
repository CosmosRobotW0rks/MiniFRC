#include "FieldDevices/Devices/DriverStation.h"
#include "Debugger.h"


volatile bool FieldDevice_DriverStation::AmpButtonPressed = false;
volatile bool FieldDevice_DriverStation::SourceButtonPressed = false;
bool FieldDevice_DriverStation::AmplifyCooldown = false;
bool FieldDevice_DriverStation::SourceCooldown = false;

void FieldDevice_DriverStation::EnabledChanged(bool enabled)
{
    DebugInfoF("Enabled changed to: %d", enabled);
    is_enabled = enabled;
    if(is_enabled)
    {
        ToggleAmpLED(false);
        ToggleSourceLED(false);
    }
}

bool FieldDevice_DriverStation::Initialize()
{
    pinMode(PIN_BTNAMP, INPUT_PULLUP);
    pinMode(PIN_BTNSOURCE, INPUT_PULLUP);
    pinMode(PIN_LEDAMP, OUTPUT);
    pinMode(PIN_LEDSOURCE, OUTPUT); 

    ToggleAmpLED(true);
    delay(1000);
    ToggleAmpLED(false);

    ToggleSourceLED(true);
    delay(1000);
    ToggleSourceLED(false);

    attachInterrupt(PIN_BTNAMP, [](){
        FieldDevice_DriverStation::AmpButtonPressed = true;
    }, RISING);

    attachInterrupt(PIN_BTNSOURCE, [](){
        FieldDevice_DriverStation::SourceButtonPressed = true;
    }, RISING);

    this->Client->RegisterPacket(Packet_DriverStation_AmpReady_ID, sizeof(Packet_DriverStation_AmpReady), [](uint8_t* data, size_t len, void* args){
        FieldDevice_DriverStation* ds = (FieldDevice_DriverStation*)args;
        ds->OnReadyToAmplify();
    }, this);

    this->Client->RegisterPacket(Packet_DriverStation_SourceReady_ID, sizeof(Packet_DriverStation_SourceReady), [](uint8_t* data, size_t len, void* args){
        FieldDevice_DriverStation* ds = (FieldDevice_DriverStation*)args;
        ds->OnReadyToTriggerSource();
    }, this);

    this->Client->RegisterPacket(Packet_DriverStation_AmplifiedPacket_ID, sizeof(Packet_DriverStation_AmplifiedPacket), [](uint8_t* data, size_t len, void* args){
        FieldDevice_DriverStation* ds = (FieldDevice_DriverStation*)args;
        ds->OnAmplify();
    }, this);

    this->Client->RegisterPacket(Packet_DriverStation_SourceTriggered_ID, sizeof(Packet_DriverStation_SourceTriggered), [](uint8_t* data, size_t len, void* args){
        FieldDevice_DriverStation* ds = (FieldDevice_DriverStation*)args;
        ds->OnSourceTrigger();
    }, this);

    xTaskCreate(task, "drvrstino", 4096, this, 0, NULL);

    return true;
}

void FieldDevice_DriverStation::task(void* args)
{
    FieldDevice_DriverStation* ds = (FieldDevice_DriverStation*)args;

    while(true)
    {
        if(FieldDevice_DriverStation::AmpButtonPressed)
        {
            if(FieldDevice_DriverStation::AmplifyCooldown) { AmpButtonPressed = false; continue;}
            ds->OnAmpButtonPress();
            FieldDevice_DriverStation::AmpButtonPressed = false;
        }
        
        if(FieldDevice_DriverStation::SourceButtonPressed)
        {
            if(FieldDevice_DriverStation::SourceCooldown) { SourceButtonPressed = false; continue;}
            ds->OnSourceButtonPress();
            FieldDevice_DriverStation::SourceButtonPressed = false;
            
        }

        if(!ds->is_enabled)
            ds->UpdateBlinkStatus();

        delay(10);
    }
    
}

void FieldDevice_DriverStation::UpdateBlinkStatus()
{
    long time = millis();

    ToggleAmpLED((time % 1500) < 750);
    ToggleSourceLED((time % 1500) > 750);
}


void FieldDevice_DriverStation::OnReadyToAmplify()
{
    DebugInfo("Ready to amplify");
    ToggleAmpLED(true);
}

void FieldDevice_DriverStation::OnReadyToTriggerSource()
{
    DebugInfo("Ready to trigger source");
    ToggleSourceLED(true);
}

void FieldDevice_DriverStation::OnAmpButtonPress()
{
    DebugInfo("Amp btn press");
    Packet_DriverStation_AmpButtonPress p;
    this->Client->SendPacket(Packet_DriverStation_AmpButtonPress_ID, &p, sizeof(Packet_DriverStation_AmpButtonPress));
}

void FieldDevice_DriverStation::OnSourceButtonPress()
{
    DebugInfo("Source btn press");
    Packet_DriverStation_SourceButtonPress p;
    this->Client->SendPacket(Packet_DriverStation_SourceButtonPress_ID, &p, sizeof(Packet_DriverStation_SourceButtonPress));
}


void FieldDevice_DriverStation::OnAmplify()
{
    FieldDevice_DriverStation::AmplifyCooldown = true;
    xTaskCreate([](void*){
    const int cooldown = 20000;

    bool lastLEDState = false;
    ToggleAmpLED(false);

    int remainingCooldown = cooldown;
    while(remainingCooldown > 0)
    {
        int delay = (remainingCooldown / 10) < 100 ? 100 : (remainingCooldown / 10);
        remainingCooldown-= delay;
        vTaskDelay(delay);

        ToggleAmpLED(!lastLEDState);
        lastLEDState = !lastLEDState;
        DebugInfoF("LED STATE CHANGED (%d)", delay);
        
    }
    
    ToggleAmpLED(false);

    FieldDevice_DriverStation::AmplifyCooldown = false;
    vTaskDelete(NULL);
    }, "amptask", 4096, this, 0, NULL);
}

void FieldDevice_DriverStation::OnSourceTrigger()
{    
    FieldDevice_DriverStation::SourceCooldown = true;

    xTaskCreate([](void*){
    ToggleSourceLED(false);
    vTaskDelay(500);
    ToggleSourceLED(true);
    vTaskDelay(500);
    ToggleSourceLED(false);
    vTaskDelay(500);
    ToggleSourceLED(true);
    vTaskDelay(500);

    ToggleSourceLED(false);

    FieldDevice_DriverStation::SourceCooldown = false;
    vTaskDelete(NULL);
    }, "sourcetask", 4096, this, 0, NULL);
}


void FieldDevice_DriverStation::ToggleAmpLED(bool state)
{
    digitalWrite(PIN_LEDAMP, state);
}

void FieldDevice_DriverStation::ToggleSourceLED(bool state)
{
    digitalWrite(PIN_LEDSOURCE, state);
}