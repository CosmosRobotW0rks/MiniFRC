#pragma once
#include "BaseFieldItem.h"

// ----- MISC -----
struct Packet_Ping_0
{
    
};

struct Packet_ClientID_1
{
    TeamColor teamColor;
    DeviceType deviceType;
    uint64_t SecurityKey;
};

struct Packet_ClientIDResponse_2
{
    uint8_t Accepted;
};



// ----- SPEAKER -----
struct Packet_Speaker_Score_3
{

};

struct Packet_Speaker_ToggleMotors_4
{
    uint8_t MotorsEnabled;
};