using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerMatchStateUpdatedPacket;

namespace MiniFRC_FMS.Modules.Game
{
    internal class MatchModule : BaseModule
    {
        public MatchState State { get; private set; } = MatchState.Standby;
        
        public Match Match { get; private set; }

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

        public void LoadMatch(Match match)
        {
            Match = match;
            State = MatchState.Loaded;
            fmsControllerModule.AnnounceMatchState(match, State);

            Logger.Log(LogLevel.INFO, $"Match loaded (ID: {match.MatchID})");
        }

        public void StartMatch()
        {
            Match.Start();
        }

        public void AbortMatch()
        {
            Match.Abort();
        }
    }
}
