using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MiniFRC_ControlApp.Comms.Packets;
using PacketCommunication;
using PacketCommunication.Client;

namespace MiniFRC_ControlApp.Comms
{
    internal static class ServerCommunication
    {
        public static PacketClient client;

        public static void Connect(IPEndPoint ep)
        {
            client = new PacketClient(ep, GetPackets(out int length));
            client.Connect();
        }

        public static void Disconnect() // No disconnect for the packetclient?? lol? TODO: Add it ASAP
        {

        }

        public static async Task<bool> Authenticate(ulong securityKey)
        {
            var resp = await client.SendPacketAndWaitForResponseAsync<FMSControllerAuthPacket, FMSControllerAuthResponsePacket>(new FMSControllerAuthPacket() { SecurityKey = securityKey }, TimeSpan.FromSeconds(5));
            if(resp.TimedOut) return false;
            if (resp.Packet.Authenticated == 1) return true;
            return false;
        }

        private static void Client_PacketReceived(object? sender, IBasePacket e)
        {

        }

        private static PacketCollection GetPackets(out int length)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            Type[] packetTypes = asm.GetTypes()
                .Where(
                    x =>
                        x.IsValueType
                        && x.Namespace.StartsWith("MiniFRC_ControlApp.Comms.Packets")
                        && x.IsAssignableTo(typeof(IBasePacket))
                ).ToArray();

            length = packetTypes.Length;

            PacketCollection packets = new PacketCollection(packetTypes);

            return packets;
        }
    }

}
