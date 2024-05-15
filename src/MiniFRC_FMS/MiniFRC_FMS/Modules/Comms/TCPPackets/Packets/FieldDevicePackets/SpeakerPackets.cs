﻿using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets // 100-109
{

    internal struct SpeakerScorePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SpeakerScorePacket;
    }

    internal struct SpeakerManuelMotorControl : IBasePacket
    {
        public byte ID => (byte)PacketIDs.SpeakerManuelMotorControl;

        public Int16 Direction { get; set; } // 0: stop, 1: In, -1: Out

        public SpeakerManuelMotorControl(Int16 direction)
        {
            Direction = direction;
        }
    }
}
