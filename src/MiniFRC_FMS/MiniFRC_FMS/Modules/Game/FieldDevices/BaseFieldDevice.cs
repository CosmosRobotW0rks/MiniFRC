using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
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

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{
    internal abstract class BaseFieldDevice
    {
        public string Name => $"{teamColor} {deviceType}";

        public Client TCPClient { get; private set; }

        public DateTime LastPing { get; private set; }

        public DeviceType deviceType { get; private set; }

        public TeamColor teamColor { get; private set; }

        public PointSource pointSource { get; private set; }

        public bool Enabled { get; private set; }

        public byte ID => (byte)(((byte)teamColor << 6) | (byte)deviceType);

        public event EventHandler<BaseFieldDevice> OnPingExpire;
        private bool Initialized = false;

        public abstract void Init();

        private int c = 0;
        public void Initialize(Client client, DeviceType _deviceType, PointSource _pointSource, TeamColor _teamColor)
        {
            if (Initialized) { Logger.Log(LogLevel.WARNING, "Device already initialized"); return; }

            this.TCPClient = client;
            this.LastPing = DateTime.Now;
            this.deviceType = _deviceType;
            this.teamColor = _teamColor;
            this.pointSource = _pointSource;

            Init();

            TCPClient.PacketReceived += TCPClient_PacketReceived; 

            Task.Run(PingCheck);
        }

        public virtual void MatchStart() { }
        public virtual void MatchStop() { }

        public async Task SetEnabledAsync(bool enabled)
        {
            try
            {
                await TCPClient.SendPacketAsync(new ClientToggleEnabledPacket(enabled));
                Enabled = enabled;
            }
            catch(Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while toggling enabled state of {Name} (Ex: {ex.Message})");
            }
        }

        private void TCPClient_PacketReceived(object? sender, PacketCommunication.IBasePacket e)
        {
            if (LastPing < LastDisconnectionWarning)
            {
                LastDisconnectionWarning = new DateTime(1, 1, 1);
                Logger.Log(LogLevel.INFO, $"\"{Name}\" was not disconnected");
            }

            LastPing = DateTime.Now;
        }

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
