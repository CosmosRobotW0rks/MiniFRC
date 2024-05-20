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

            match.Points[TeamColor.RED].PointAdded += (sender, p) => GetModule<FMSControllerAppModule>().AnnouncePointAddedAsync(TeamColor.RED, p).Wait();
            match.Points[TeamColor.RED].PointRemoved += (sender, id) => GetModule<FMSControllerAppModule>().AnnouncePointRemovedAsync(TeamColor.RED, id).Wait();

            match.Points[TeamColor.BLUE].PointAdded += (sender, p) => GetModule<FMSControllerAppModule>().AnnouncePointAddedAsync(TeamColor.BLUE, p).Wait();
            match.Points[TeamColor.BLUE].PointRemoved += (sender, id) => GetModule<FMSControllerAppModule>().AnnouncePointRemovedAsync(TeamColor.BLUE, id).Wait();

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

        public int AddPoints(TeamColor team, PointSource source, int points)
        {
            if (match != null)
            {
                return match.Points[team].AddPoint(source, points);
            }

            return -1;
        }

        public bool RemovePoint(TeamColor team, int pointID)
        {
            if (match != null)
            {
                return match.Points[team].DeletePointByID(pointID);
            }

            return false;
        }

        public void SetStage(TeamColor team, int count)
        {
            if (match != null)
            {
                match.Points[team].DeletePoints(x => x.PointSource == PointSource.Stage);

                if (count != 0)
                    match.Points[team].AddPoint(PointSource.Stage, Config.Field.StageClimbPerRobot * count);
            }
        }

        public void SetTrap(TeamColor team, bool state)
        {
            if (match != null)
            {
                match.Points[team].DeletePoints(x => x.PointSource == PointSource.Trap);

                if (state)
                    match.Points[team].AddPoint(PointSource.Trap, Config.Field.Trap);
            }
        }

        public void PointsApproved()
        {
            if (match == null || match.State != MatchState.PointsCalculating) return;
            var fieldModule = GetModule<FieldModule>();
            var audismodule = GetModule<AuDisModule>();
            audismodule.AnnounceAfterMatch(match);
            audismodule.SwitchPage(AuDisPage.AfterMatch);

            GetModule<DataSavingModule>().Matches.Add(match);


            match.ResetYellowCardsOfDisqualifiedTeams();

            match = null;
            fmsControllerModule.AnnounceMatchStateAsync(null, MatchState.Standby).Wait();
        }


        #region Match Events
        private void Match_OnAbort(object? sender, EventArgs e)
        {
            Logger.Log($"Match Aborted (RED P: {match.Points[TeamColor.RED].PointsSum} / BLUE P: {match.Points[TeamColor.BLUE].PointsSum})");
            
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
            Logger.Log($"Match Ended (RED P: {match.Points[TeamColor.RED].PointsSum} / BLUE P: {match.Points[TeamColor.BLUE].PointsSum})");
            match.SwitchToPointsCalculating();

            var fieldModule = GetModule<FieldModule>();
            var audismodule = GetModule<AuDisModule>();

            audismodule.UpdateMatchState();
            Task.Delay(4000).Wait();
            audismodule.SwitchPage(AuDisPage.CalculatingPoints);

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
