using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{

    enum MatchState
    {

    }

    internal class Match
    {
        public int MatchID { get; private set; }

        public Team TeamRED1 { get; private set; }
        public Team TeamRED2 { get; private set; }
        public Team[] REDAllience { get { return [TeamRED1, TeamRED2]; } }

        public Team TeamBLUE1 { get; private set; }
        public Team TeamBLUE2 { get; private set; }
        public Team[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2]; } }

        public Match(int matchID, Team teamRED1, Team teamRED2, Team teamBLUE1, Team teamBLUE2)
        {
            TeamRED1 = teamRED1;
            TeamRED2 = teamRED2;
            TeamBLUE1 = teamBLUE1;
            TeamBLUE2 = teamBLUE2;

            MatchID = matchID;
        }
    }
}
