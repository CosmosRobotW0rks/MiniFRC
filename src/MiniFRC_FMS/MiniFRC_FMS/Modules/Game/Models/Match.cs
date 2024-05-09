using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerLoadMatchPacket;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerMatchStateUpdatedPacket;
using MatchType = MiniFRC_FMS.Modules.Comms.TCPPackets.FMSControllerLoadMatchPacket.MatchType;

namespace MiniFRC_FMS.Modules.Game.Models
{

    internal class Match
    {
        public MatchState State { get; private set; } = MatchState.Loaded;

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
        public int REDPoints => REDPointsList.Sum(x => x.Points);

        public byte TeamBLUE1 { get; private set; }
        public byte TeamBLUE2 { get; private set; }
        public byte[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2]; } }
        public int BLUEPoints => BLUEPointsList.Sum(x => x.Points);



        private List<Point> REDPointsList = new();
        private List<Point> BLUEPointsList = new();



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
        public event EventHandler<byte>? OnCountdownUpdate;
        public event EventHandler? OnStart;
        public event EventHandler? OnAbort;
        public event EventHandler? OnEnd;
        public event EventHandler<int> OnPointUpdate;

        private bool IsAborted { get; set; } = false;

        private async Task MatchTask()
        {
            State = MatchState.Countdown;
            while (State == MatchState.Countdown && RemainingCountdown > 0 && !IsAborted)
            {
                if (!IsAborted) OnCountdownUpdate?.Invoke(this, RemainingCountdown);

                RemainingCountdown--;

                await Task.Delay(1000);
            }
            if (IsAborted) return;

            State = MatchState.Running;

            while (State == MatchState.Running && RemainingTime > 0 && !IsAborted)
            {
                if (!IsAborted) OnTimeUpdate?.Invoke(this, RemainingTime);

                RemainingTime--;

                await Task.Delay(1000);
            }
            if (IsAborted) return;

            State = MatchState.Standby;

            OnEnd?.Invoke(this, null);
        }

        public void Start()
        {
            if (State != MatchState.Loaded) return;
            RemainingTime = MatchDuration;
            RemainingCountdown = 3;
            Task.Run(MatchTask);
        }

        public void Abort()
        {
            if (State != MatchState.Countdown && State != MatchState.Running) return;

            IsAborted = true;
            State = MatchState.Standby;
            OnAbort?.Invoke(this, null);
        }

        internal void AddPoints(TeamColor team, Point point)
        {
            if(State != MatchState.Running) return;

            switch(team)
            {
                case TeamColor.RED:
                    REDPointsList.Add(point);
                    break;
                case TeamColor.BLUE:
                    BLUEPointsList.Add(point);
                    break;
            }
        }
    }
}
