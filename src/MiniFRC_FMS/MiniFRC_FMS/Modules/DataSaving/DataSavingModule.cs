using MiniFRC_FMS.Modules.Game.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.DataSaving
{
    internal class DataSavingModule : BaseModule
    {
        public DataSavingItem<Team> Teams;
        public DataSavingItem<Match> Matches;

        protected override bool Init()
        {
            if (!Directory.Exists(Paths.BasePath)) Directory.CreateDirectory(Paths.BasePath);

            Teams = new DataSavingItem<Team>(Paths.TeamsFilePath);
            Matches = new DataSavingItem<Match>(Paths.MatchesFilePath);

            return true;
        }


    }
}
