using MiniFRC_FMS.Models;
using MiniFRC_FMS.Modules;
using SimpleWebServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.FieldItems
{
    internal class Speaker : BaseFieldItem
    {
        public override string Host { get; protected set; }
        public override int Port { get; protected set; }

        public TeamColor teamColor { get; private set; }

        public event Action? NoteScored;


        public Speaker(string host, int port, TeamColor teamColor) : base(host, port)
        {
            this.teamColor = teamColor;

            WebServerModule.AddRoute($"/speaker/{(int)teamColor}/score", ScoreCB, SimpleWebServer.HttpMethod.POST);
        }

        private async Task ScoreCB(HttpListenerContext ctx)
        {
            _ = ctx.CreateStringResponseAsync("OK");

            NoteScored?.Invoke();
        }

        

    }
}
