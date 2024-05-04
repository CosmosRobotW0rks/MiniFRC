using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Speaker
{
    internal struct SpeakerScorePacket : IBasePacket
    {
        public byte ID => 3;
    }
}
