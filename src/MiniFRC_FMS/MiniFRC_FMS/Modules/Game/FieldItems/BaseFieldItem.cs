using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Misc;
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
        public DateTime LastPing { get; private set; }
        public string Nickname { get; private set; }

        public event EventHandler<BaseFieldItem> OnPingExpire;

        public BaseFieldItem(Client client, string nickname)
        {
            this.TCPClient = client;
            this.Nickname = nickname;
            LastPing = DateTime.Now;

            TCPServerModule.AttachPacketCallback<PingPacket>((_, _) =>
            {
                LastPing = DateTime.Now;
            }, TCPClient);

            Task.Run(PingCheck);
        }


        private async Task PingCheck()
        {
            while (true)
            {
                if ((DateTime.Now - LastPing).TotalMilliseconds > Config.PingExpireTimeMS)
                {
                    OnPingExpire?.Invoke(this, this);
                    return;
                }

                await Task.Delay(200);
            }
        }
    }
}
