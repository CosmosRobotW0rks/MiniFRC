using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerLoadMatchPacket;
using MatchType = MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerLoadMatchPacket.MatchType;

namespace MiniFRC_FMS.Modules.Game.Models
{

    internal class Match
    {
        public MatchType Type { get; private set; }
        public bool IsPractice { get; private set; }
        public bool IsRematch { get; private set; }

        public UInt16 MatchDuration { get; private set; }

        public UInt16 RemainingTime { get; private set; }
        public byte RemainingCountdown { get; private set; }

        public byte MatchID { get; private set; }

        public byte TeamRED1 { get; private set; }
        public byte TeamRED2 { get; private set; }
        public byte[] REDAllience { get { return [TeamRED1, TeamRED2]; } }

        public byte TeamBLUE1 { get; private set; }
        public byte TeamBLUE2 { get; private set; }
        public byte[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2]; } }

        public Match(byte matchID, MatchType type, bool isPractice, bool isRematch, UInt16 matchDuration, byte teamRED1,byte teamRED2, byte teamBLUE1, byte teamBLUE2)
        {
            MatchID = matchID;
            Type = type;
            IsPractice = isPractice;
            IsRematch = isRematch;
            MatchDuration = matchDuration;

            TeamRED1 = teamRED1;
            TeamRED2 = teamRED2;
            TeamBLUE1 = teamBLUE1;
            TeamBLUE2 = teamBLUE2;

            RemainingTime = matchDuration;
            RemainingCountdown = 3;
        }

        public Match() { }


        public event EventHandler<UInt16>? OnTimeUpdate;
        public event EventHandler<UInt16>? OnCountdownUpdate;
        public event EventHandler? OnStart;
        public event EventHandler? OnAbort;
        public event EventHandler? OnEnd;

        public void Start()
        {

        }

        public void Abort()
        {

        }
    }
}
