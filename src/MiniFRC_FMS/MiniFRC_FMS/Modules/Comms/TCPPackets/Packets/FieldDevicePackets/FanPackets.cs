using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets
{
    internal struct FanToggleElectricityPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FanToggleElectricityPacket;

        public byte State { get; set; }

        public FanToggleElectricityPacket(bool state)
        {
            State = state ? (byte)1 : (byte)0;
        }
    }
}
