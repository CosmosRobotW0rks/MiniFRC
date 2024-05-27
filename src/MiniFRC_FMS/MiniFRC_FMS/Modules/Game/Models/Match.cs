using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerLoadMatchPacket;
using static MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerMatchStateUpdatedPacket;
using MatchType = MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FMSControllerLoadMatchPacket.MatchType;

namespace MiniFRC_FMS.Modules.Game.Models
{

    internal class Match
    {
        [JsonProperty]
        public MatchState State { get; private set; } = MatchState.Loaded;

        [JsonProperty]
        public MatchType Type { get; private set; }

        [JsonProperty]
        public bool IsPractice { get; private set; }

        [JsonProperty]
        public bool IsRematch { get; private set; }

        public UInt16 MatchDuration { get; private set; }

        [JsonProperty]
        public int MatchID { get; private set; }

        [JsonProperty]
        public int TeamRED1 { get; private set; }

        [JsonProperty]
        public int TeamRED2 { get; private set; }

        [JsonProperty]
        public int TeamRED3 { get; private set; }

        [JsonProperty]
        public int[] REDAllience { get { return [TeamRED1, TeamRED2, TeamRED3]; } }


        [JsonProperty]
        public int TeamBLUE1 { get; private set; }

        [JsonProperty]
        public int TeamBLUE2 { get; private set; }

        [JsonProperty]
        public int TeamBLUE3 { get; private set; }

        public int[] BLUEAllience { get { return [TeamBLUE1, TeamBLUE2, TeamBLUE3]; } }

        [JsonProperty]
        public List<int> DisqualifiedTeams = new();


        [JsonProperty]
        private Dictionary<TeamColor, PointCollection> _points = new Dictionary<TeamColor, PointCollection>()
        {
            { TeamColor.RED, new PointCollection() },
            { TeamColor.BLUE, new PointCollection() }
        };

        [JsonProperty]
        public IReadOnlyDictionary<TeamColor, PointCollection> Points => _points.AsReadOnly();

        public Match(byte matchID, MatchType type, bool isPractice, bool isRematch, UInt16 matchDuration, int teamRED1, int teamRED2, int teamRED3, int teamBLUE1, int teamBLUE2, int teamBLUE3)
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

        [JsonProperty]
        private bool IsAborted { get; set; } = false;

        [JsonProperty]
        public byte RemainingCountdown { get; private set; }

        [JsonProperty]
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

                State = MatchState.PointsCalculating;

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
            if (State != MatchState.Countdown && State != MatchState.Running && State != MatchState.PointsCalculating) return;

            IsAborted = true;
            State = MatchState.Standby;
            OnAbort?.Invoke(this, null);
        }

        public void SwitchToPointsCalculating()
        {
            State = MatchState.PointsCalculating;
        }

        public void SwitchToAftermatch()
        {
            State = MatchState.AfterMatch;
        }

        public void ResetYellowCardsOfDisqualifiedTeams()
        {
            var dsmodule = ModulesMain.Instance.GetModule<DataSavingModule>();
            REDAllience.Select(x => dsmodule.Teams.GetWhere(y => y.ID == x).FirstOrDefault()).ToList().ForEach(x =>
            {
                if (x != null && x.YellowCardCount >= 2) x.YellowCardCount = 0;
            });

            dsmodule.Teams.SaveItems();

        }

        public int GetTeamIDByTeamIndex(TeamColor color, int teamIndex)
        {

            if (teamIndex < 0 || teamIndex > 2) return -1;

            switch(color)
            {
                case TeamColor.RED: return REDAllience[teamIndex];
                case TeamColor.BLUE: return BLUEAllience[teamIndex];
            }

            return -1;
        }
    }
}
