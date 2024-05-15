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
        SpeakerManuelMotorControl,
        __PLACEHOLDER7,
        __PLACEHOLDER8,

        // AMP
        AmpScorePacket,
        AmpManuelMotorControl,
        __PLACEHOLDER9,
        __PLACEHOLDER10,


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
