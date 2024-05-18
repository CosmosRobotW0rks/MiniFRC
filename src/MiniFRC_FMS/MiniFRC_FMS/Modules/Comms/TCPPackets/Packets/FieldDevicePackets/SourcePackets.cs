﻿using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets
{
    internal struct SourceDropPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SourceDropPacket;
    }
}
