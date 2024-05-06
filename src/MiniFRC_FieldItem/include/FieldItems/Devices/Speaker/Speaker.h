#pragma once
#include <Arduino.h>
#include "PacketClient.h"
#include "Debugger.h"

namespace FieldItem_Speaker
{
    void NoteDetectorTask(void *pvParameters);

    bool Initialize()
    {
        xTaskCreate(NoteDetectorTask, "NoteDetectorTask", 4096, nullptr, 1, nullptr);
    }

    inline void Periodic()
    {
        
    }

    
    void NoteDetectorTask(void *pvParameters)
    {
        while (true)
        {
            delay(10);
        }
    }
}