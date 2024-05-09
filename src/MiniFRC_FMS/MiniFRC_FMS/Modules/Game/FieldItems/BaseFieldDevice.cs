using MiniFRC_FMS.Modules.Comms;
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
    internal abstract class BaseFieldDevice
    {
        public Client TCPClient { get; private set; }
        public DateTime LastPing { get; private set; }

        public string Nickname { get; private set; }

        public DeviceType deviceType { get; private set; }

        public TeamColor teamColor { get; private set; }

        public PointSource pointSource { get; private set; }

        public event EventHandler<BaseFieldDevice> OnPingExpire;

        public BaseFieldDevice(Client client, string nickname, DeviceType _deviceType,  PointSource _pointSource, TeamColor _teamColor)
        {
            this.TCPClient = client;
            LastPing = DateTime.Now;
            Nickname = nickname;
            this.deviceType = _deviceType;
            this.teamColor = _teamColor;
            this.pointSource = _pointSource;

            Init();

            // TODO: FIX THIS
           ModulesMain.Instance.GetModule<TCPServerModule>().AttachPacketCallback<PingPacket>((_, _) =>
            {
                LastPing = DateTime.Now;
            }, TCPClient);

            Task.Run(PingCheck);
        }

        public BaseFieldDevice()
        {

        }

        public abstract void Init();

        protected void Score(int points)
        {
            Point p = new Point(pointSource, points);

            ModulesMain.Instance.GetModule<MatchModule>().AddPoints(teamColor, p);
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
