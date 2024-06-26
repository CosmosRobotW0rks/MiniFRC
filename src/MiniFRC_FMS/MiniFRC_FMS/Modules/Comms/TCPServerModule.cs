﻿using MiniFRC_FMS.Utils;
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

        public event EventHandler<Client>? ClientConnected;
        public event EventHandler<Client>? ClientDisconnected;

        protected override bool Init()
        {
            server = new PacketServer(IPEndPoint.Parse(Config.TCPServerEndpoint), GetPackets(out int packetCount));

            server.StartListening();
            server.ServerErrored += Server_ServerErrored;
            server.ClientConnected += (s, e) => ClientConnected?.Invoke(s, e);
            server.ClientDisconnected += (s, e) => ClientDisconnected?.Invoke(s, e);
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            Logger.Log($"Loaded {packetCount} TCP packets", LogLevel.DEBUG);
            return true;
        }

        private void Server_ClientDisconnected(object? sender, Client e)
        {
            Logger.Log($"Client Disconnected ({e.GetHashCode()})", LogLevel.DEBUG);
        }

        private void Server_ClientConnected(object? sender, Client e)
        {
            Logger.Log($"Client Connected ({e.GetHashCode()})", LogLevel.DEBUG);
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
