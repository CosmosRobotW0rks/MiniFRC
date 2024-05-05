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

            // TODO: FIX THIS
           ModulesMain.Instance.GetModule<TCPServerModule>().AttachPacketCallback<PingPacket>((_, _) =>
            {
                LastPing = DateTime.Now;
            }, TCPClient);

            Task.Run(PingCheck);
        }


        DateTime LastDisconnectionWarning = new DateTime(1, 1, 1);

        private async Task PingCheck()
        {
            while (true)
            {
                if (LastPing > LastDisconnectionWarning && (DateTime.Now - LastPing).TotalMilliseconds > Config.PingExpireTimeMS)
                {
                    LastDisconnectionWarning = DateTime.Now;

                    OnPingExpire?.Invoke(this, this);
                }

                await Task.Delay(200);
            }
        }
    }
}
