using MiniFRC_FMS.Modules.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    internal static class MatchModule
    {
        public static Match Match { get; private set; }

        public static event EventHandler<Match> OnMatchLoad;
        public static event EventHandler<Match> OnMatchStart;
        public static event EventHandler<Match> OnMatchEnd;
        public static event EventHandler<Match> OnMatchTick;


        public static bool Initialize()
        {
            return true;
        }

        public static void LoadMatch(Match match)
        {
            Match = match;
        }

        public static void PauseMatch()
        {

        }
    }
}
