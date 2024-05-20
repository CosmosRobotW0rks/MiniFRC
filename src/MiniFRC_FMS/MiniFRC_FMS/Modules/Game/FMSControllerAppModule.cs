using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Modules.Game.FieldDevices;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    [ModuleInitPriority(48)]
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

            TCPServerModule?.AttachPacketCallback<FMSControllerEnableDisableDevicePacket>(HandleEnableDisableDeviceReq);

            TCPServerModule?.AttachPacketCallback<FMSControllerRemovePointPacket>(HandleRemovePoint);

            TCPServerModule?.AttachPacketCallback<FMSControllerApprovePointsPacket>(HandlePointsApproval);

            TCPServerModule?.AttachPacketCallback<FMSControllerSwitchAuDisPagePacket>(HandleAuDisPageSwitch);

            TCPServerModule?.AttachPacketCallback<FMSControllerToggleElectricityPacket>(HandleToggleElectricity);


            TCPServerModule.ClientDisconnected += TCPServerModule_ClientDisconnected;
            return true;
        }

        private void HandleToggleElectricity(Client client, FMSControllerToggleElectricityPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            var fieldModule = GetModule<FieldModule>();

            if(fieldModule.Fan == null) { Logger.Log("FAN is null, cannot toggle electricity"); return; }

            fieldModule.Fan.ToggleElectricity(packet.State == (byte)1).Wait();
        }

        private void HandleAuDisPageSwitch(Client client, FMSControllerSwitchAuDisPagePacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            var auDisModule = GetModule<AuDisModule>();

            auDisModule.SwitchPage(packet.auDisPage);
        }

        private void HandlePointsApproval(Client client, FMSControllerApprovePointsPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            var matchModule = GetModule<MatchModule>();

            matchModule.PointsApproved();
        }

        private void HandleRemovePoint(Client client, FMSControllerRemovePointPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            var matchModule = GetModule<MatchModule>();

            matchModule.RemovePoint(packet.Alliance,packet.PointID);
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
            await AnnounceMatchStateAsync(matchModule.match, matchModule.State, client);

        }
        async void HandleEnableDisableDeviceReq(Client client, FMSControllerEnableDisableDevicePacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            var fieldModule = GetModule<FieldModule>();

            if (packet.DeviceCount == 0) await fieldModule.ToggleEnabledAllAsync(packet.Enabled);
            else
            {
                var allDevices = fieldModule.GetAllFieldDevices();

                List<Task> t = new List<Task>();

                for (int i = 0; i<packet.DeviceCount; i++)
                {
                    byte deviceID = packet.DeviceIDs[i];
                    BaseFieldDevice? device = allDevices.Where(x => x.ID == deviceID).FirstOrDefault();
                    if (device == null)
                    {
                        Logger.Log(LogLevel.WARNING, "Couldn't find the device with the ID " + deviceID.ToString());
                        return;
                    }

                    t.Add(device.SetEnabledAsync(packet.Enabled));
                }


                await Task.WhenAll(t);
            }

            await client.SendPacketAsync(new FMSControllerEnableDisableDeviceResponsePacket());
        }


        async void HandleMatchLoad(Client client, FMSControllerLoadMatchPacket packet)
        {
            if (!FMSControllerAppClients.Contains(client)) return;

            Match match = new Match(packet.MatchID, packet.matchType, packet.Practice == 1, packet.Rematch == 1,packet.MatchDuration, packet.ID_RED1, packet.ID_RED2, packet.ID_RED3, packet.ID_BLUE1, packet.ID_BLUE2, packet.ID_BLUE3);
            var dataSaving = GetModule<DataSavingModule>();
            var matchModule = GetModule<MatchModule>();

            int c = (dataSaving.Teams.GetWhere(x => x.ID == packet.ID_RED1 || x.ID == packet.ID_RED2 || x.ID == packet.ID_RED3 || x.ID == packet.ID_BLUE1 || x.ID == packet.ID_BLUE2 || x.ID == packet.ID_BLUE3)).Count;
            if(c  != 6)
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

        public async Task AnnouncePacketAsync<T>(T packet) where T : IBasePacket, new()
        {
            try
            {
                List<Task> tasks = new();

                foreach (Client cli in FMSControllerAppClients)
                {
                    if (cli == null)
                    {
                        continue;
                    }

                    tasks.Add(cli.SendPacketAsync(packet));
                }

                await Task.WhenAll(tasks);
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"Failed to announce {typeof(T).Name} packet to FMS Controller Clients / ex: {ex.Message}");
            }
        }

        public async Task AnnouncePointAddedAsync(TeamColor alliance, PointCollection.Point p)
        {
            try
            {
                var packet = new FMSControllerPointAddedPacket(p.PointID, alliance, p.PointSource, p.Points);

                await AnnouncePacketAsync(packet);
            }
            catch (Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce point added to FMS Controllers (ex: {ex.Message})"); }
        }

        public async Task AnnouncePointRemovedAsync(TeamColor alliance, int pointID)
        {
            try
            {
                var packet = new FMSControllerPointRemovedPacket(pointID, alliance);

                await AnnouncePacketAsync(packet);
            }
            catch (Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce point removed to FMS Controllers (ex: {ex.Message})"); }
        }

        public async Task AnnounceAuDisPageChangeAsync(AuDisPage newPage)
        {
            try
            {
                var packet = new FMSControllerAuDisPageUpdatedPacket() { auDisPage = newPage};

                await AnnouncePacketAsync(packet);
            }
            catch (Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce audis page changed to FMS Controllers (ex: {ex.Message})"); }

        }

        public async Task AnnounceMatchStateAsync(Match? match, FMSControllerMatchStateUpdatedPacket.MatchState state, Client singleClient = null)
        {
            try
            {
                var packet = new FMSControllerMatchStateUpdatedPacket()
                {
                    matchState = state,
                    ID_RED1 = match?.TeamRED1 ?? 0,
                    ID_RED2 = match?.TeamRED2 ?? 0,
                    ID_RED3 = match?.TeamRED3 ?? 0,
                    ID_BLUE1 = match?.TeamBLUE1 ?? 0,
                    ID_BLUE2 = match?.TeamBLUE2 ?? 0,
                    ID_BLUE3 = match?.TeamBLUE3 ?? 0,
                    MatchID = match?.MatchID ?? 0,
                    MatchDuration = match?.MatchDuration ?? 0,
                    RemainingTime = match?.RemainingTime ?? 0,
                    Countdown = match?.RemainingCountdown ?? 0,
                    matchType = match?.Type ?? 0,
                    Practice = (match?.IsPractice ?? false) ? (byte)1 : (byte)0,
                    Rematch = (match?.IsRematch ?? false) ? (byte)1 : (byte)0,

                    REDPoints = match?.Points?[TeamColor.RED].PointsSum ?? 0,
                    BLUEPoints = match?.Points[TeamColor.BLUE].PointsSum ?? 0

                };

                if (singleClient == null)
                {
                    await AnnouncePacketAsync(packet);
                }
                else await singleClient.SendPacketAsync(packet);
            }
            catch(Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce match state to FMS Controllers (ex: {ex.Message})"); }
}

        public async Task AnnounceDeviceStatesAsync(Dictionary<(DeviceType, TeamColor), DateTime> dict, Client singleClient = null)
        {
            try
            {

                FMSControllerDeviceLastseenUpdatedPacket packet = new FMSControllerDeviceLastseenUpdatedPacket(dict);



                if (singleClient == null)
                {
                    await AnnouncePacketAsync(packet);
                }
                else await singleClient.SendPacketAsync(packet);
            }
            catch(Exception ex) { Logger.Log(LogLevel.WARNING, $"Failed to announce device states to FMS Controllers (ex: {ex.Message})"); }
        }
    }
}
