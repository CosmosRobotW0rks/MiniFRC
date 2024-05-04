using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.Models
{
    internal class Team
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public Team(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
