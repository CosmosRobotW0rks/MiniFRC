
using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.DataSaving;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    internal class RefereeModule : BaseModule
    {

        public enum Command
        {
            Penalty,
            Card,
            Stage,
            Trap,

        }

        protected override bool Init()
        {
            GetModule<WebSocketModule>().OnDataReceived += WsModule_OnDataReceived;



            handlers.Add(Command.Penalty, HandlePenalty);
            handlers.Add(Command.Card, HandleCard);
            handlers.Add(Command.Stage, HandleStage);
            handlers.Add(Command.Trap, HandleTrap);


            return true;
        }



        private void HandlePenalty(RefereeCommand command)
        {
            GetModule<MatchModule>().AddPoints(command.allianceColor == TeamColor.NONE ? throw new Exception("Invalid team") : command.allianceColor == TeamColor.RED ? TeamColor.BLUE : TeamColor.RED, PointSource.Penalty, Config.Field.PenaltyPoints);
        }


        private void HandleCard(RefereeCommand command)
        {
            int? teamID = GetModule<MatchModule>().match?.GetTeamIDByTeamIndex(command.allianceColor, (int)command.content.teamIndex);
            if(teamID == null) return;
            
            Team t = GetModule<DataSavingModule>().Teams.GetWhere(x => x.ID == teamID).FirstOrDefault();

            if ((bool)command.content.isRedCard) t.YellowCardCount = 2;
            else t.YellowCardCount += 1;

            GetModule<DataSavingModule>().Teams.SaveItems();
        }

        private void HandleStage(RefereeCommand command)
        {
            GetModule<MatchModule>().SetStage(command.allianceColor, (int)command.content.robotCount);
        }

        private void HandleTrap(RefereeCommand command)
        {
            GetModule<MatchModule>().SetTrap(command.allianceColor, (bool)command.content.trapState);
        }





        // -------------------------------------

        private delegate void RefereeCommandHandler(RefereeCommand command);


        Dictionary<Command, RefereeCommandHandler> handlers = new();




        private void WsModule_OnDataReceived(object? sender, WebSocketModule.WSDataReceivedEventArgs e)
        {
            string received = e.Data;
            try
            {
                RefereeCommand? command = Newtonsoft.Json.JsonConvert.DeserializeObject<RefereeCommand>(received);
                if (command == null) throw new Exception("Failed to parse command, command is null");

                if (handlers.ContainsKey(command.EnumCommand))
                {
                    handlers[command.EnumCommand](command);
                }
                else
                {
                    Logger.Log(LogLevel.WARNING, "Received unknown referee command: " + command.command);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.WARNING, "Failed to handle referee command: " + ex.Message);
            }

        }


        private class RefereeCommand
        {
            public string command;
            public TeamColor allianceColor;

            public dynamic content;

            public Command EnumCommand => Enum.Parse<Command>(value: command);
        }
    }
}
