using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.FieldItemPackets
{

    internal struct SpeakerScorePacket : IBasePacket
    {
        public byte ID => 3;
    }

    internal struct SpeakerToggleMotorsPacket : IBasePacket
    {
        public byte ID => 4;

        public bool MotorsEnabled { get; set; }

        public SpeakerToggleMotorsPacket(bool motorsEnabled)
        {
            MotorsEnabled = motorsEnabled;
        }
    }
}
