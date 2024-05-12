using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerMatchStateUpdatedPacket;
using Match = MiniFRC_FMS.Modules.Game.Models.Match;
namespace MiniFRC_FMS.Modules.Game
{
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
            fmsControllerModule.AnnounceMatchState(match, State);


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
            match = null;
            fmsControllerModule.AnnounceMatchState(match, State);
        }

        private void Match_OnEnd(object? sender, EventArgs e)
        {
            match = null;
            fmsControllerModule.AnnounceMatchState(match, State);
        }

        private void Match_OnStart(object? sender, EventArgs e)
        {
            fmsControllerModule.AnnounceMatchState(match, State);
        }

        private void Match_OnTimeUpdate(object? sender, ushort e)
        {
            fmsControllerModule.AnnounceMatchState(match, State);
        }

        private void Match_OnCountdownUpdate(object? sender, byte e)
        {
            fmsControllerModule.AnnounceMatchState(match, State);
        }

        private void Match_OnPointUpdate(object? sender, EventArgs e)
        {
            fmsControllerModule.AnnounceMatchState(match, State);
        }
        #endregion
    }
}
