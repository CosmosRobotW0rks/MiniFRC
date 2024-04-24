using MiniFRC_FMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.FieldItems
{
    internal class Speaker : BaseFieldItem
    {
        public override string Host { get; protected set; }
        public override int Port { get; protected set; }

        public TeamColor teamColor { get; private set; }

        public Speaker(string host, int port, TeamColor teamColor) : base(host, port)
        {
            this.teamColor = teamColor;
        }

    }
}
