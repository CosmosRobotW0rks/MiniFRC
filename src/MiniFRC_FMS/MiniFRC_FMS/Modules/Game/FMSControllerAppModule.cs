using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    internal class FMSControllerAppModule : BaseModule
    {
        private TCPServerModule? TCPServerModule = null;

        private List<Client> FMSControllerAppClients = new List<Client>();

        protected override bool Init()
        {
            TCPServerModule = GetModule<TCPServerModule>();
            TCPServerModule.ClientDisconnected += TCPServerModule_ClientDisconnected;
            TCPServerModule.ClientConnected += TCPServerModule_ClientConnected;


            TCPServerModule?.AttachPacketCallback<FMSControllerAuthPacket>(HandleFMSControllerAuth);

            TCPServerModule?.AttachPacketCallback<FMSControllerLoadMatchPacket>(HandleMatchLoad);
            TCPServerModule?.AttachPacketCallback<FMSControllerStartStopMatchPacket>(HandleMatchStartStop);

            return true;
        }

        private void TCPServerModule_ClientConnected(object? sender, Client e)
        {
            Logger.Log("Cli cıonnct");
        }

        private void TCPServerModule_ClientDisconnected(object? sender, Client e)
        {
            if (FMSControllerAppClients.Contains(e))
            {
                FMSControllerAppClients.Remove(e);
                Logger.Log("FMS Controller App Disconnected");
            }
        }

        async void HandleMatchLoad(Client client, FMSControllerLoadMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            Logger.Log("Received Match Load Packet");

        }

        async void HandleMatchStartStop(Client client, FMSControllerStartStopMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;
            Logger.Log("Received Match Start/Stop Packet");
        }

        async void HandleFMSControllerAuth(Client client, FMSControllerAuthPacket packet)
        {
            if (packet.SecurityKey != Config.SecurityKey)
            {
                await client.SendPacketAsync(new FMSControllerAuthResponsePacket(false));
                return;
            }

            await client.SendPacketAsync(new FMSControllerAuthResponsePacket(true));
            FMSControllerAppClients.Add(client);

            Logger.Log("FMS Controller App Connected");
        }
    }
}
