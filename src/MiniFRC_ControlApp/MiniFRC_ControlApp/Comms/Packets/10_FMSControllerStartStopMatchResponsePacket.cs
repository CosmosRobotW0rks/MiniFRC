using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp.Comms.Packets
{
    internal struct FMSControllerStartStopMatchResponsePacket : IBasePacket
    {
        public byte ID => 10;

        public byte Success { get; set; }
    }
}
