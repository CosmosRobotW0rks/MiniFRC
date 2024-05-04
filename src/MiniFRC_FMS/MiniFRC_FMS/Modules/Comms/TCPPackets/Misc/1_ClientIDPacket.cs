using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Misc
{
    internal struct ClientIDPacket : IBasePacket
    {
        public byte ID => 1;

        public TeamColor TeamColor { get; set; }

        public DeviceType DeviceType { get; set; }

        public ulong SecurityKey { get; set; }
    }
}
