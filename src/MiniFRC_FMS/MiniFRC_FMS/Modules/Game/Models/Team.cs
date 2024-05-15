using MiniFRC_FMS.Modules.DataSaving;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{
    internal class Team
    {
        [JsonProperty]
        public byte ID { get; private set; }

        [JsonProperty]
        public string Name { get; private set; }

        public int RP
        {
            get
            {
                var matches = ModulesMain.Instance.GetModule<DataSavingModule>().Matches.GetWhere(x => x.BLUEAllience.Contains(ID) || x.REDAllience.Contains(ID));
                int RP = 0;

                foreach(Match match in matches)
                {
                    if (match.REDPoints == match.BLUEPoints) RP += 1;
                    else if (match.REDAllience.Contains(ID) && match.REDPoints > match.BLUEPoints) RP += 2;
                    else if (match.BLUEAllience.Contains(ID) && match.BLUEPoints > match.REDPoints) RP += 2;
                    
                }

                return RP;
            }
        }
        public Team(byte id, string name)
        {
            ID = id;
            Name = name;
        }

        public Team() { }
    }
}
