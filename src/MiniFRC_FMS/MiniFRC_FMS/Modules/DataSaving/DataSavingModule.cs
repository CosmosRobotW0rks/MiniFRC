using MiniFRC_FMS.Modules.Game.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.DataSaving
{
    [ModuleInitPriority(0)]
    internal class DataSavingModule : BaseModule
    {
        public DataSavingItem<Team> Teams;
        public DataSavingItem<Match> Matches;

        protected override bool Init()
        {
            if (!Directory.Exists(Paths.BasePath)) Directory.CreateDirectory(Paths.BasePath);

            Teams = new DataSavingItem<Team>(Paths.TeamsFilePath);
            Matches = new DataSavingItem<Match>(Paths.MatchesFilePath);


            foreach(Match m in Matches.GetAll())
            {
                Console.WriteLine($"MatchID: {m.MatchID} / RED: {m.TeamRED1} {m.TeamRED2} {m.TeamRED3} / BLUE: {m.TeamBLUE1} / {m.TeamBLUE2} / {m.TeamBLUE3} / RP: {m.Points[TeamColor.RED].PointsSum} / BP: {m.Points[TeamColor.BLUE].PointsSum}");
            }

            return true;
        }


    }
}
