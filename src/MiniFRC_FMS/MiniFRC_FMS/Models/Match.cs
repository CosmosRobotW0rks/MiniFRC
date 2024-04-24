using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Models
{
    internal class Match
    {
        public Team TeamRED { get; private set; }
        public Team TeamBLUE { get; private set; }


        public Match(Team TeamRED, Team TeamBLUE)
        {
            this.TeamRED = TeamRED;
            this.TeamBLUE = TeamBLUE;
        }
    }
}
