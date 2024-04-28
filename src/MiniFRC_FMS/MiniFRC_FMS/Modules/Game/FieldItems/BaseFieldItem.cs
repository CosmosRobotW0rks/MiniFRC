using MiniFRC_FMS.Modules.Comms.TCPPackets;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using SimpleWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HttpMethod = SimpleWebServer.HttpMethod;

namespace MiniFRC_FMS.Modules.Game.FieldItems
{
    internal abstract class BaseFieldItem
    {
        public Client TCPClient { get; private set; }

        public async Task<bool> Ping()
        {
            return true;
        }


        public BaseFieldItem(Client client)
        {
            this.TCPClient = client;
        }
    }
}
