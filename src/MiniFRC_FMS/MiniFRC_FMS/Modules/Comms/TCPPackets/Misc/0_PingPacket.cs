﻿using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Misc
{
    internal struct PingPacket : IBasePacket
    {
        public byte ID => 0;
    }
}
