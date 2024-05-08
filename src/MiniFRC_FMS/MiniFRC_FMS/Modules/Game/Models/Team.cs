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

        public Team(byte id, string name)
        {
            ID = id;
            Name = name;
        }

        public Team() { }
    }
}
