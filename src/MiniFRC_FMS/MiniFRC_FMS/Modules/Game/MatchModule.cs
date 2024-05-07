using MiniFRC_FMS.Modules.Game.Models;
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
        public MatchState State { get; set; } = MatchState.Standby;

        public Match Match { get; private set; }

        public event EventHandler<Match> OnMatchLoad;
        public event EventHandler<Match> OnMatchStart;
        public event EventHandler<Match> OnMatchEnd;
        public event EventHandler<Match> OnMatchTick;


        protected override bool Init()
        {
            return true;
        }

        public void LoadMatch(Match match)
        {
            Match = match;
            State = MatchState.Loaded;
        }

        public void PauseMatch()
        {

        }
    }
}
