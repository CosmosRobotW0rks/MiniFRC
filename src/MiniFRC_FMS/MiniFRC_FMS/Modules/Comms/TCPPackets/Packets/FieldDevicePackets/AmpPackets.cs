using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets
{
    internal struct AmpScorePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SpeakerScorePacket;
    }

    internal struct AmpManuelMotorControl : IBasePacket
    {
        public byte ID => (byte)PacketIDs.AmpManuelMotorControl;

        public Int16 Direction { get; set; } // 0: stop, 1: In, -1: Out

        public AmpManuelMotorControl(Int16 direction)
        {
            Direction = direction;
        }
    }
}
