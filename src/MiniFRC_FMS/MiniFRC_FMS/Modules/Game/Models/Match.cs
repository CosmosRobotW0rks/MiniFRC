using MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController;
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
        public FMSControllerLoadMatchPacket.MatchType Type { get; private set; }

        public int MatchID { get; private set; }

        public byte TeamRED1 { get; private set; }
        public byte TeamRED2 { get; private set; }
        public byte[] REDAllience { get { return [TeamRED1, TeamRED2]; } }

        public byte TeamBLUE1 { get; private set; }
        public byte TeamBLUE2 { get; private set; }
        public byte[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2]; } }

        public Match(int matchID, FMSControllerLoadMatchPacket.MatchType type, byte teamRED1,byte teamRED2, byte teamBLUE1, byte teamBLUE2)
        {
            MatchID = matchID;
            Type = type;

            TeamRED1 = teamRED1;
            TeamRED2 = teamRED2;
            TeamBLUE1 = teamBLUE1;
            TeamBLUE2 = teamBLUE2;

        }

        public Match() { }
    }
}
