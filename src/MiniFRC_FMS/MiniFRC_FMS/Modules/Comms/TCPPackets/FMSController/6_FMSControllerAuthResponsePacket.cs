using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController
{
    internal struct FMSControllerAuthResponsePacket : IBasePacket
    {
        public byte ID => 6;

        public byte Authenticated { get; set; }

        public FMSControllerAuthResponsePacket(bool authenticated)
        {
            Authenticated = authenticated ? (byte)1 : (byte)0;
        }
        public FMSControllerAuthResponsePacket() { }
    }
}
