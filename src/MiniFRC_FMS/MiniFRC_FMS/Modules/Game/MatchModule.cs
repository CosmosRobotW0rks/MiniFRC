using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerMatchStateUpdatedPacket;
using Match = MiniFRC_FMS.Modules.Game.Models.Match;
namespace MiniFRC_FMS.Modules.Game
{
    [ModuleInitPriority(49)]
    internal class MatchModule : BaseModule
    {
        
        public Match? match { get; private set; }
        public MatchState State { get { return match?.State ?? MatchState.Standby; } }

        private FMSControllerAppModule? fmsControllerModule;
        private TCPServerModule? tcpServerModule;

        protected override bool Init()
        {
            fmsControllerModule = GetModule<FMSControllerAppModule>();
            tcpServerModule = GetModule<TCPServerModule>();

            fmsControllerModule.OnMatchLoad += (sender, match) => LoadMatch(match);
            fmsControllerModule.OnMatchStart += (sender, e) => StartMatch();
            fmsControllerModule.OnMatchAbort += (sender, e) => AbortMatch();

            return true;
        }

        public void LoadMatch(Match _match)
        {
            this.match = _match;
            _ = fmsControllerModule.AnnounceMatchStateAsync(match, State);
            GetModule<AuDisModule>().UpdateMatchState();


            match.OnCountdownUpdate += Match_OnCountdownUpdate;
            match.OnTimeUpdate += Match_OnTimeUpdate;
            match.OnStart += Match_OnStart;
            match.OnEnd += Match_OnEnd;
            match.OnAbort += Match_OnAbort;
            match.OnPointUpdate += Match_OnPointUpdate;

            Logger.Log(LogLevel.INFO, $"Match loaded (ID: {_match.MatchID})");
        }


        public void StartMatch()
        {
            if(match != null) match.Start();
        }

        public void AbortMatch()
        {
            if(match != null) match.Abort();
        }

        public void AddPoints(TeamColor team, Point point)
        {
            if(match != null)
            {
                match.AddPoints(team, point);
            }
        }

        #region Match Events
        private void Match_OnAbort(object? sender, EventArgs e)
        {
            Logger.Log($"Match Aborted (RED P: {match.REDPoints} / BLUE P: {match.BLUEPoints})");
            match = null;


            var fieldModule = GetModule<FieldModule>();

            Task.WaitAll(
            fieldModule.ToggleEnabledAllAsync(false),
            fieldModule.AnnounceMatchStartStopAsync(false),
            fmsControllerModule.AnnounceMatchStateAsync(match, State),
            Task.Run(() => GetModule<AuDisModule>().UpdateMatchState()));
        }

        private void Match_OnEnd(object? sender, EventArgs e)
        {
            Logger.Log($"Match Ended (RED P: {match.REDPoints} / BLUE P: {match.BLUEPoints})");
            GetModule<AuDisModule>().UpdateMatchState();


            if (match == null) { Logger.Log(LogLevel.WARNING, "Couldn't save the match, match is null"); }
            else GetModule<DataSavingModule>().Matches.Add(match);


            match = null;


            var fieldModule = GetModule<FieldModule>();

            Task.WaitAll(
            fieldModule.ToggleEnabledAllAsync(false),
            fieldModule.AnnounceMatchStartStopAsync(false),
            fmsControllerModule.AnnounceMatchStateAsync(match, State));

        }

        private void Match_OnStart(object? sender, EventArgs e)
        {
            var fieldModule = GetModule<FieldModule>();
            Task.WaitAll(
            fieldModule.ToggleEnabledAllAsync(true),
            fieldModule.AnnounceMatchStartStopAsync(true),
            fmsControllerModule.AnnounceMatchStateAsync(match, State),
            Task.Run(() => GetModule<AuDisModule>().UpdateMatchState()));
        }

        private void Match_OnTimeUpdate(object? sender, ushort e)
        {
            Task.Run(() => GetModule<AuDisModule>().UpdateMatchState());
            _ = fmsControllerModule.AnnounceMatchStateAsync(match, State);
        }

        private void Match_OnCountdownUpdate(object? sender, byte e)
        {
            Task.Run(() => GetModule<AuDisModule>().UpdateMatchState());
            _ = fmsControllerModule.AnnounceMatchStateAsync(match, State);
        }

        private void Match_OnPointUpdate(object? sender, EventArgs e)
        {
            Task.Run(() => GetModule<AuDisModule>().UpdateMatchState());
            _ = fmsControllerModule.AnnounceMatchStateAsync(match, State);
        }
        #endregion
    }
}
