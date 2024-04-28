using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Misc
{
    internal struct ClientIDResponsePacket : IBasePacket
    {
        public byte ID => 2;

        public bool Accepted { get; set; }

        public ClientIDResponsePacket(bool accepted)
        {
            this.Accepted = accepted;
        }
    }
}
