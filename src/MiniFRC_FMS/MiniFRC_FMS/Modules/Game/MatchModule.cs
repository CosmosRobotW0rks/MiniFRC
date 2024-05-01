using MiniFRC_FMS.Modules.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    internal class MatchModule : BaseModule
    {
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
        }

        public void PauseMatch()
        {

        }
    }
}
