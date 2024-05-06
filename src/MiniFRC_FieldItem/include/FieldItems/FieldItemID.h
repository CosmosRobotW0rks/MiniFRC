#pragma once
#include <Arduino.h>

typedef bool (*FieldItemInitialize)();
typedef void (*FieldItemPeriodic)();

enum DeviceType : uint8_t
{
    NONE = 0,
    Speaker = 1
};

enum TeamColor : uint8_t
{
    RED = 0,
    BLUE = 1
};