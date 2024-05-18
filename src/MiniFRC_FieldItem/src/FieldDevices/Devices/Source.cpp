#include "FieldDevices/Devices/Source.h"

bool FieldDevice_Source::Initialize()
{
    drop_server.attach(13, 500, 2500);
    return true;
}

void FieldDevice_Source::EnabledChanged(bool enabled)
{
    
}

bool FieldDevice_Source::Calibrate()
{
    return true;
}

void FieldDevice_Source::Drop()
{
    goto_angle(pos_b);
    goto_angle(180);
    goto_angle(pos_b);
    goto_angle(pos_b + 2 * shake);
    goto_angle(pos_b);
    goto_angle(pos_b - shake);
    goto_angle(pos_b);
    
    goto_angle(pos_a);
    goto_angle(0);
    goto_angle(pos_a);
    goto_angle(pos_a - 2 * shake);
    goto_angle(pos_a);
    goto_angle(pos_a + shake);
    goto_angle(pos_a);  
}

void FieldDevice_Source::goto_angle(int angle) {
    while (cur_angle != angle) {
        drop_server.write(cur_angle);
        cur_angle += cur_angle > angle ? -1 : 1;
        delay(10);
    }
}