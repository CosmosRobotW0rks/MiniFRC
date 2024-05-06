using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController
{
    internal struct FMSControllerAuthPacket : IBasePacket
    {
        public byte ID => 5;

        public ulong SecurityKey { get; set; }
    }
}
