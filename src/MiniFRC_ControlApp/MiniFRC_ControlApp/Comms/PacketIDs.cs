using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp.Comms
{

    enum PacketIDs
    {
        // MISC
        PingPacket = 0,

        // GENERIC FIELD DEVICE PACKETS
        ClientIDPacket,
        ClientIDResponsePacket,
        ClientCalibratePacket,
        ClientCalibrateResponsePacket,
        ClientToggleEnabledPacket,
        __PLACEHOLDER1,
        __PLACEHOLDER2,
        __PLACEHOLDER3,
        __PLACEHOLDER4,
        __PLACEHOLDER5,


        //   ------ FIELD DEVICE PACKETS -----

        // SPEAKER
        SpeakerScorePacket,
        __PLACEHOLDER6,
        __PLACEHOLDER7,
        __PLACEHOLDER8,

        // AMP
        AmpScorePacket,
        AmpAmplifiedPacket,
        __PLACEHOLDER9,
        __PLACEHOLDER10,

        // DRIVER STATION
        DriverStationAmpReadyPacket,
        DriverStationSourceReadyPacket,

        DriverStationAmpPressedPacket,
        DriverStationAmplifiedPacket,

        DriverStationSourcePressedPacket,
        DriverStationSourceTriggeredPacket,


        // FMS CONTROLLER PACKETS
        FMSControllerAuthPacket,
        FMSControllerAuthResponsePacket,
        FMSControllerLoadMatchPacket,
        FMSControllerLoadMatchResponsePacket,
        FMSControllerStartStopMatchPacket,
        FMSControllerStartStopMatchResponsePacket,
        FMSControllerMatchStateUpdatedPacket,
        FMSControllerAuDisPageUpdatedPacket,
        FMSControllerDeviceLastseenUpdatedPacket,
        FMSControllerEnableDisableDevicePacket,
        FMSControllerEnableDisableDeviceResponsePacket,


    }
}
