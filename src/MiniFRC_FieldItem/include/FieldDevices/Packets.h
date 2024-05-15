#pragma once
#include "BaseFieldDevice.h"

// ----- MISC -----

const uint8_t Packet_Ping_ID = 0;
struct Packet_Ping
{
    
};

const uint8_t Packet_ClientID_ID = 1;
struct Packet_ClientID
{
    uint8_t teamColor;
    uint8_t deviceType;
    uint64_t SecurityKey;
};

const uint8_t Packet_ClientIDResponse_ID = 2;
struct Packet_ClientIDResponse
{
    uint8_t Accepted;
};

const uint8_t Packet_ClientCalibrate_ID = 3;
struct Packet_ClientCalibrate
{
    
};

const uint8_t Packet_ClientCalibrateResponse_ID = 4;
struct Packet_ClientCalibrateResponse
{
    uint8_t Success;
};

const uint8_t Packet_ClientToggleEnabled_ID = 5;
struct Packet_ClientToggleEnabled
{
    uint8_t State;
};



// ----- SPEAKER -----

const uint8_t Packet_Speaker_Score_ID = 11;
struct Packet_Speaker_Score
{

};

const uint8_t Packet_Speaker_ToggleMotors_ID = 12;
struct Packet_Speaker_ToggleMotors
{
    uint8_t MotorsEnabled;
};