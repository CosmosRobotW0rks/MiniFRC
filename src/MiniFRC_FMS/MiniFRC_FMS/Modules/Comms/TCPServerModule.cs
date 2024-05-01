using MiniFRC_FMS.Utils;
using PacketCommunication;
using PacketCommunication.Server;
using SimpleWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms
{
    [ModuleInitPriority(2)]
    internal class TCPServerModule : BaseModule
    {
        private PacketServer server;

        protected override bool Init()
        {
            server = new PacketServer(IPEndPoint.Parse(Config.TCPServerEndpoint), GetPackets(out int packetCount));

            server.StartListening();
            server.ServerErrored += Server_ServerErrored;
            Logger.Log($"Loaded {packetCount} TCP packets", LogLevel.DEBUG);
            return true;
        }

        public void AttachPacketCallback<T>(Action<Client, T> callback, Client? cli = null) where T : IBasePacket
        {
            server.AttachPacketCallback(callback, cli);
        }

        private void Server_ServerErrored(object? sender, Exception e)
        {
            Logger.Log("TCP Server Error: " + e.Message, LogLevel.ERROR);
        }

        private PacketCollection GetPackets(out int length)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            Type[] packetTypes = asm.GetTypes()
                .Where(
                    x =>
                        x.IsValueType
                        && x.Namespace.StartsWith("MiniFRC_FMS.Modules.Comms.TCPPackets")
                        && x.IsAssignableTo(typeof(IBasePacket))
                ).ToArray();

            length = packetTypes.Length;

            PacketCollection packets = new PacketCollection(packetTypes);
            
            return packets;
        }
    }
}
