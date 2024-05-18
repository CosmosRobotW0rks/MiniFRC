using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets
{

    internal struct DriverStationAmpReadyPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationAmpReadyPacket;
    }

    internal struct DriverStationSourceReadyPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationSourceReadyPacket;
    }

    internal struct DriverStationAmpPressedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationAmpPressedPacket;
    }

    internal struct DriverStationAmplifiedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationAmplifiedPacket;
    }

    internal struct DriverStationSourcePressedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationSourcePressedPacket;
    }

    internal struct DriverStationSourceTriggeredPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.DriverStationSourceTriggeredPacket;
    }
}
