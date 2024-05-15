using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets;
using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication.Server;
using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniFRC_FMS.Utils;

namespace MiniFRC_FMS.Modules.Game
{
    [ModuleInitPriority(50)]
    internal class AuDisModule : BaseModule
    {
        protected override bool Init()
        {
            wsModule = GetModule<WebSocketModule>();
            dsModule = GetModule<DataSavingModule>();
            matchModule = GetModule<MatchModule>();
            return true;
        }

        private WebSocketModule wsModule;
        private DataSavingModule dsModule;
        private MatchModule matchModule;

        public AuDisPage Page { get; private set; }

        public void SwitchPage(AuDisPage page)
        {
            AuDisCommand cmd = new AuDisCommand(AuDisCommand.Command.updatePage,new
            {
                Page = (int)page
            });

            wsModule.Announce(cmd);
            Logger.Log(LogLevel.DEBUG, $"Switched AuDis Page from {this.Page} to {page}");
            this.Page = page;
        }

        

        public void UpdateLeaderboard()
        {
            Team[] teams = dsModule.Teams.GetAll().OrderByDescending(x => x.RP).ToArray();

            AuDisCommand cmd = new AuDisCommand(AuDisCommand.Command.updateLeaderboard, new
            {
                Teams = teams
            }); 
            
            wsModule.Announce(cmd);
            Logger.Log(LogLevel.DEBUG, $"Updated AuDis leaderboard");
        }

        public void UpdateMatchState()
        {
            Team[] allTeams = dsModule.Teams.GetAll().ToArray();

            Match match = matchModule.match;

            if (match == null)
            {
                Logger.Log(LogLevel.INFO, "Match is null, nothing to update");
                return;
            }

            AuDisCommand cmd = new AuDisCommand(AuDisCommand.Command.updateMatchState, new
            {
                MatchState = match.State.ToString(), // Standby, Loaded, Countdown, Running

                MatchID = match.MatchID,
                IsPractice = match.IsPractice,
                IsRematch = match.IsRematch,

                MatchType = match.Type.ToString(), // Qualification, Semifinal, Final

                RedTeams = match.REDAllience.Select(x => allTeams.Where(y => y.ID == x).FirstOrDefault()).ToArray(),
                BlueTeams = match.BLUEAllience.Select(x => allTeams.Where(y => y.ID == x).FirstOrDefault()).ToArray(),

                RedPoints = match.REDPoints,
                BluePoints = match.BLUEPoints,

                CD = match.RemainingCountdown,
                RT = match.RemainingTime,

                
            });

            wsModule.Announce(cmd);
        }


        private class AuDisCommand
        {
            public enum Command
            {
                updatePage,

                updateLeaderboard,
                updateMatchState
            }

            public string command { get; private set; }
            public object content { get; private set; }

            public AuDisCommand(Command cmd, object obj)
            {
                command = cmd.ToString();
                content = obj;
            }
        }
    }
}
