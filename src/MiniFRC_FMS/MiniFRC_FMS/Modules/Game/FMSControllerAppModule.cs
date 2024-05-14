using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Modules.Game.FieldDevices;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    [ModuleInitPriority(100)]
    internal class FMSControllerAppModule : BaseModule
    {
        private TCPServerModule? TCPServerModule = null;

        private List<Client> FMSControllerAppClients = new List<Client>();

        public event EventHandler<Match>? OnMatchLoad;
        public event EventHandler? OnMatchStart;
        public event EventHandler? OnMatchAbort;

        protected override bool Init()
        {
            TCPServerModule = GetModule<TCPServerModule>();


            TCPServerModule?.AttachPacketCallback<FMSControllerAuthPacket>(HandleFMSControllerAuth);

            TCPServerModule?.AttachPacketCallback<FMSControllerLoadMatchPacket>(HandleMatchLoad);
            TCPServerModule?.AttachPacketCallback<FMSControllerStartStopMatchPacket>(HandleMatchStartStop);
            TCPServerModule.ClientDisconnected += TCPServerModule_ClientDisconnected;
            return true;
        }

        private void TCPServerModule_ClientDisconnected(object? sender, Client e)
        {
            if (FMSControllerAppClients.Contains(e))
            {
                FMSControllerAppClients.Remove(e);
                Logger.Log("FMS Controller App Disconnected");
            }
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
            var matchModule = GetModule<MatchModule>();
            var fieldModule = GetModule<FieldModule>();


            await Task.Delay(100);
            AnnounceMatchState(matchModule.match, matchModule.State, client);

        }

        async void HandleMatchLoad(Client client, FMSControllerLoadMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            Match match = new Match(packet.MatchID, packet.matchType, packet.Practice == 1, packet.Rematch == 1,packet.MatchDuration, packet.ID_RED1, packet.ID_RED2, packet.ID_BLUE1, packet.ID_BLUE2);
            var dataSaving = GetModule<DataSavingModule>();
            var matchModule = GetModule<MatchModule>();

            int c = (dataSaving.Teams.GetWhere(x => x.ID == packet.ID_RED1 || x.ID == packet.ID_RED2 || x.ID == packet.ID_BLUE1 || x.ID == packet.ID_BLUE2)).Count;
            if(c  != 4)
            {
                await client.SendPacketAsync(new FMSControllerLoadMatchResponsePacket(FMSControllerLoadMatchResponsePacket.MatchLoadStatus.IncorrectTeamIDs));
                return;
            }

            if(dataSaving.Matches.GetWhere(x => x.MatchID == packet.MatchID).Count != 0)
            {
                await client.SendPacketAsync(new FMSControllerLoadMatchResponsePacket(FMSControllerLoadMatchResponsePacket.MatchLoadStatus.MatchExists));
                return;
            }

            if(matchModule.State != FMSControllerMatchStateUpdatedPacket.MatchState.Standby && matchModule.State != FMSControllerMatchStateUpdatedPacket.MatchState.Loaded)
            {
                await client.SendPacketAsync(new FMSControllerLoadMatchResponsePacket(FMSControllerLoadMatchResponsePacket.MatchLoadStatus.IncorrectMatchState));
                return;
            }

            Task t = client.SendPacketAsync(new FMSControllerLoadMatchResponsePacket(FMSControllerLoadMatchResponsePacket.MatchLoadStatus.Success));

            OnMatchLoad?.Invoke(this, match);
            await t;
        }

        async void HandleMatchStartStop(Client client, FMSControllerStartStopMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;
            Task t = client.SendPacketAsync(new FMSControllerStartStopMatchResponsePacket(true));
            if (packet.State == 1) OnMatchStart?.Invoke(this, null);
            else OnMatchAbort?.Invoke(this, null);
        }


        public void AnnounceMatchState(Match? match, FMSControllerMatchStateUpdatedPacket.MatchState state, Client singleClient = null)
        {
            try
            {
                var packet = new FMSControllerMatchStateUpdatedPacket()
                {
                    matchState = state,
                    ID_RED1 = match?.TeamRED1 ?? 0,
                    ID_RED2 = match?.TeamRED2 ?? 0,
                    ID_BLUE1 = match?.TeamBLUE1 ?? 0,
                    ID_BLUE2 = match?.TeamBLUE2 ?? 0,
                    MatchID = match?.MatchID ?? 0,
                    MatchDuration = match?.MatchDuration ?? 0,
                    RemainingTime = match?.RemainingTime ?? 0,
                    Countdown = match?.RemainingCountdown ?? 0,
                    matchType = match?.Type ?? 0,
                    Practice = (match?.IsPractice ?? false) ? (byte)1 : (byte)0,
                    Rematch = (match?.IsRematch ?? false) ? (byte)1 : (byte)0,

                    REDPoints = match?.REDPoints ?? 0,
                    BLUEPoints = match?.BLUEPoints ?? 0

                };

                if (singleClient == null)
                {
                    Task.WaitAll(FMSControllerAppClients.Select(x => x.SendPacketAsync(packet)).ToArray());
                }
                else singleClient.SendPacketAsync(packet).Wait();
            }
            catch(Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce match state to FMS Controllers (ex: {ex.Message})"); }
}

        public void AnnounceDeviceStates(Dictionary<(DeviceType, TeamColor), DateTime> dict, Client singleClient = null)
        {
            try
            {

                FMSControllerDeviceLastseenUpdatedPacket packet = new FMSControllerDeviceLastseenUpdatedPacket(dict);



                if (singleClient == null)
                {
                    Task.WaitAll(FMSControllerAppClients.Select(x => x.SendPacketAsync(packet)).ToArray());
                }
                else singleClient.SendPacketAsync(packet).Wait();
            }
            catch(Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce device states to FMS Controllers (ex: {ex.Message})"); }
        }
    }
}
