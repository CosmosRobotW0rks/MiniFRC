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
        PLACEHOLDER1,
        PLACEHOLDER2,
        PLACEHOLDER3,
        PLACEHOLDER4,
        PLACEHOLDER5,


        //   ------ FIELD DEVICE PACKETS -----

        // SPEAKER
        SpeakerScorePacket,
        SpeakerToggleMotorsPacket,
        PLACEHOLDER6,
        PLACEHOLDER7,

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


    }
}
