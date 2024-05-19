using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerLoadMatchPacket;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerMatchStateUpdatedPacket;
using MatchType = MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerLoadMatchPacket.MatchType;

namespace MiniFRC_FMS.Modules.Game.Models
{

    internal class Match
    {
        public MatchState State { get; private set; } = MatchState.Loaded;

        public MatchType Type { get; private set; }
        public bool IsPractice { get; private set; }
        public bool IsRematch { get; private set; }

        public UInt16 MatchDuration { get; private set; }

        public byte MatchID { get; private set; }

        public byte TeamRED1 { get; private set; }
        public byte TeamRED2 { get; private set; }
        public byte TeamRED3 { get; private set; }
        public byte[] REDAllience { get { return [TeamRED1, TeamRED2, TeamRED3]; } }

        public byte TeamBLUE1 { get; private set; }
        public byte TeamBLUE2 { get; private set; }
        public byte TeamBLUE3 { get; private set; }
        public byte[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2, TeamBLUE3]; } }



        private Dictionary<TeamColor, PointCollection> _points = new Dictionary<TeamColor, PointCollection>()
        {
            { TeamColor.RED, new PointCollection() },
            { TeamColor.BLUE, new PointCollection() }
        };
        public IReadOnlyDictionary<TeamColor, PointCollection> Points => _points.AsReadOnly();
        public Match(byte matchID, MatchType type, bool isPractice, bool isRematch, UInt16 matchDuration, byte teamRED1,byte teamRED2, byte teamRED3, byte teamBLUE1, byte teamBLUE2, byte teamBLUE3)
        {
            MatchID = matchID;
            Type = type;
            IsPractice = isPractice;
            IsRematch = isRematch;
            MatchDuration = matchDuration;

            TeamRED1 = teamRED1;
            TeamRED2 = teamRED2;
            TeamRED3 = teamRED3;
            TeamBLUE1 = teamBLUE1;
            TeamBLUE2 = teamBLUE2;
            TeamBLUE3 = teamBLUE3;
        }

        public Match() { }


        public event EventHandler<UInt16>? OnTimeUpdate;
        public event EventHandler<byte>? OnCountdownUpdate;
        public event EventHandler? OnStart;
        public event EventHandler? OnAbort;
        public event EventHandler? OnEnd;
        public event EventHandler? OnPointUpdate;

        private bool IsAborted { get; set; } = false;

        public byte RemainingCountdown { get; private set; }
    
        public UInt16 RemainingTime { get; private set; }
        private async Task MatchTask()
        {
            try
            {
                RemainingCountdown = 3 + 1;
                RemainingTime = (ushort)(MatchDuration + 1);



                State = MatchState.Countdown;
                while (State == MatchState.Countdown && RemainingCountdown > 1 && !IsAborted)
                {
                    RemainingCountdown--;

                    try
                    {
                        if (!IsAborted) OnCountdownUpdate?.Invoke(this, RemainingCountdown);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(LogLevel.ERROR, $"Error while invoking OnCountdownUpdate event: {ex.Message}");
                    }



                    await Task.Delay(1000);
                }
                RemainingCountdown = 0;
                if (IsAborted) return;

                State = MatchState.Running;

                try
                {
                    OnStart?.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.ERROR, $"Error while invoking OnStart event: {ex.Message}");
                }

                while (State == MatchState.Running && RemainingTime > 1 && !IsAborted)
                {
                    RemainingTime--;
                    if (!IsAborted) OnTimeUpdate?.Invoke(this, RemainingTime);


                    await Task.Delay(1000);
                }
                RemainingTime = 0;
                if (IsAborted) return;

                State = MatchState.Standby;

                try
                {
                    OnEnd?.Invoke(this, null);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.ERROR, $"Error while invoking OnEnd event: {ex.Message}");
                }
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while running match task (Ex: {ex.Message}) / Aborting");
                IsAborted = true;
                return;
            }
        }

        public void Start()
        {
            if (State != MatchState.Loaded) return;
            Task.Run(MatchTask);
        }

        public void Abort()
        {
            if (State != MatchState.Countdown && State != MatchState.Running) return;

            IsAborted = true;
            State = MatchState.Standby;
            OnAbort?.Invoke(this, null);
        }
    }
}
