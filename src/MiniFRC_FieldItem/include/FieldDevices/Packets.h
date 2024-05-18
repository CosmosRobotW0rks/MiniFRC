#pragma once
#include "BaseFieldDevice.h"

// ----- MISC -----

const uint8_t Packet_Ping_ID = 0;
struct Packet_Ping
{
    
};

// GENERIC FIELD DEVICE

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

const uint8_t Packet_ClientInitializationStatus_ID = 6;
struct Packet_ClientInitializationStatus
{
    uint8_t Success;
};



// ----- SPEAKER -----

const uint8_t Packet_Speaker_Score_ID = 11;
struct Packet_Speaker_Score
{

};


// ----- AMP -----

const uint8_t Packet_Amp_Score_ID = 15;
struct Packet_Amp_Score
{

};

const uint8_t Packet_Amp_AmplificationEnded_ID = 16;
struct Packet_Amp_AmplificationEnded
{

};


// ----- DRIVER STATION -----

const uint8_t Packet_DriverStation_AmpReady_ID = 19;
struct Packet_DriverStation_AmpReady
{
    
};

const uint8_t Packet_DriverStation_SourceReady_ID = 20;
struct Packet_DriverStation_SourceReady
{
    
};

const uint8_t Packet_DriverStation_AmpButtonPress_ID = 21;
struct Packet_DriverStation_AmpButtonPress
{
    
};

const uint8_t Packet_DriverStation_AmplifiedPacket_ID = 22;
struct Packet_DriverStation_AmplifiedPacket
{
    
};

const uint8_t Packet_DriverStation_SourceButtonPress_ID = 23;
struct Packet_DriverStation_SourceButtonPress
{
    
};



const uint8_t Packet_DriverStation_SourceTriggered_ID = 24;
struct Packet_DriverStation_SourceTriggered
{
    
};


// ----- SOURCE -----
const uint8_t Packet_Source_Drop_ID = 25;
struct Packet_Source_Drop
{
    
};