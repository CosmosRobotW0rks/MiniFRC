using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController
{
    internal struct FMSControllerStartStopMatchResponsePacket : IBasePacket
    {
        public byte ID => 10;

        public bool Success { get; set; }
    }
}
