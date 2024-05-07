using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
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


            TCPServerModule?.AttachPacketCallback<FMSControllerAuthPacket>(HandleFMSControllerAuth);

            TCPServerModule?.AttachPacketCallback<FMSControllerLoadMatchPacket>(HandleMatchLoad);
            TCPServerModule?.AttachPacketCallback<FMSControllerStartStopMatchPacket>(HandleMatchStartStop);

            return true;
        }

        async void HandleMatchLoad(Client client, FMSControllerLoadMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            Logger.Log(
                $"Received Match Load Packet\n" +
                $"IDs: {packet.ID_RED1} / {packet.ID_RED2} / {packet.ID_BLUE1} / {packet.ID_BLUE2}\n" +
                $"MatchID: {packet.MatchID}\n" +
                $"Match Duration: {packet.MatchDuration}\n" +
                $"MatchType: {packet.matchType.ToString()}\n" +
                $"IsPractice: {packet.Practice == 1}\n" +
                $"IsRematch: {packet.Rematch == 1}"
                );

            await client.SendPacketAsync(new FMSControllerLoadMatchResponsePacket(FMSControllerLoadMatchResponsePacket.MatchLoadStatus.Success));

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
