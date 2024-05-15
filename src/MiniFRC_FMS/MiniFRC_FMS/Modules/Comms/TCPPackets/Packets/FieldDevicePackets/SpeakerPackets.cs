using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets // 100-109
{

    internal struct SpeakerScorePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SpeakerScorePacket;
    }

    internal struct SpeakerToggleMotorsPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SpeakerToggleMotorsPacket;

        public bool MotorsEnabled { get; set; }

        public SpeakerToggleMotorsPacket(bool motorsEnabled)
        {
            MotorsEnabled = motorsEnabled;
        }
    }
}
