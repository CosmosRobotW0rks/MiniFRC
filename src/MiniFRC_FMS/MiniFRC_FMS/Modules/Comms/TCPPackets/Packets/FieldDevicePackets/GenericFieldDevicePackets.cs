using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets // 70-99
{


    internal struct ClientIDPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientIDPacket;

        public TeamColor TeamColor { get; set; }

        public DeviceType DeviceType { get; set; }

        public ulong SecurityKey { get; set; }
    }

    internal struct ClientIDResponsePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientIDResponsePacket;

        public byte Accepted { get; set; }


        public ClientIDResponsePacket(bool accepted)
        {
            Accepted = accepted ? (byte)1 : (byte)0;
        }
        
    }

    internal struct ClientCalibratePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientCalibratePacket;
    }

    internal struct ClientCalibrateResponsePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientCalibrateResponsePacket;

        public byte Success { get; set; }

        public ClientCalibrateResponsePacket(bool accepted)
        {
            Success = accepted ? (byte)1 : (byte)0;
        }
    }


    internal struct ClientToggleEnabledPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientToggleEnabledPacket;

        public byte Enabled { get; set; }

        public ClientToggleEnabledPacket(bool enabled)
        {
            Enabled = enabled ? (byte)1 : (byte)0;
        }
    }

    internal struct ClientInitializationStatusPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.ClientInitializationStatusPacket;

        public byte Initialized { get; set; }

        public ClientInitializationStatusPacket(bool initialized)
        {
            Initialized = initialized ? (byte)1 : (byte)0;
        }
    }
}
