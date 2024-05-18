#include "FieldDevices/Devices/Source.h"

bool FieldDevice_Source::Initialize()
{
    drop_server.attach(13, 500, 2500);

    Client->RegisterPacket(Packet_Source_Drop_ID, sizeof(Packet_Source_Drop), [](uint8_t* buf, size_t len, void* args){
        FieldDevice_Source* source = (FieldDevice_Source*)args;
        source->drop = true;
    }, this);

    xTaskCreate(DropTask, "DropTask", 4096, this, 0, NULL);
    DebugInfo("DROP TASK CREATED");

    return true;
}

void FieldDevice_Source::EnabledChanged(bool enabled)
{
    
}

bool FieldDevice_Source::Calibrate()
{
    return true;
}

void FieldDevice_Source::DropTask(void* args)
{
    FieldDevice_Source* source = (FieldDevice_Source*)args;

    while(true)
    {
        if(!source->drop)
        {
            delay(100);

            continue;
        }

        DebugInfo("Starting drop");

        source->goto_angle(pos_a);
        source->goto_angle(0);
        source->goto_angle(pos_a);
        source->goto_angle(pos_a - 2 * shake);
        source->goto_angle(pos_a);
        source->goto_angle(pos_a + shake);
        source->goto_angle(pos_a); 

        source->goto_angle(pos_b);
        source->goto_angle(180);
        source->goto_angle(pos_b);
        source->goto_angle(pos_b + 2 * shake);
        source->goto_angle(pos_b);
        source->goto_angle(pos_b - shake);
        source->goto_angle(pos_b); 

        source->drop = false;
        
        DebugInfo("drop ended");
    }
    

    
}

void FieldDevice_Source::goto_angle(int angle) {
    while (cur_angle != angle) {
        drop_server.write(cur_angle);
        cur_angle += cur_angle > angle ? -1 : 1;
        delay(10);
    }
}