using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.FieldDevices;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{

    internal class FieldModule : BaseModule
    {
        [FieldDevice(DeviceType.Speaker, PointSource.Speaker, TeamColor.RED)]
        public Speaker? REDSpeaker { get; private set; } = null;


        [FieldDevice(DeviceType.Speaker, PointSource.Speaker, TeamColor.BLUE)]
        public Speaker? BLUESpeaker { get; private set; } = null;



        protected override bool Init()
        {
            GetModule<TCPServerModule>()?.AttachPacketCallback<ClientIDPacket>(HandleClientIdentification);

            Task.Run(UpdateFMSControlersWithFieldDataAsync);
            return true;
        }

        private async Task UpdateFMSControlersWithFieldDataAsync()
        {
            FMSControllerAppModule fmsControllerModule = GetModule<FMSControllerAppModule>();
            while (true)
            {
                try
                {

                    fmsControllerModule.AnnounceDeviceStates(GetDevicesInfo());
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    Logger.Log(LogLevel.ERROR, $"An error occured while updating FMS Controllers with field device data (Ex: {ex.Message})");
                }
            }
        }


        public Dictionary<(DeviceType, TeamColor), DateTime> GetDevicesInfo()
        {
            Dictionary<(DeviceType, TeamColor), DateTime> dict = new();

            PropertyInfo[] props = this.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(FieldDeviceAttribute)) != null && x.PropertyType.IsSubclassOf(typeof(BaseFieldDevice))).ToArray();

            foreach (PropertyInfo info in props)
            {
                var attr = info.GetCustomAttribute<FieldDeviceAttribute>();
                BaseFieldDevice? device = (BaseFieldDevice?)info.GetValue(this);

                DateTime lastPing = device?.LastPing ?? DateTime.MinValue;

                dict.Add((attr.deviceType, attr.teamColor), lastPing);
            }

            return dict;
        }

        void HandlePingExpire(object? sender, BaseFieldDevice fieldDevice)
        {
            Logger.Log(LogLevel.WARNING, $"Potential disconnection from \"{fieldDevice.Name}\"");
        }

        async void HandleClientIdentification(Client client, ClientIDPacket packet)
        {
            try
            {
                if (packet.SecurityKey != Config.SecurityKey)
                {
                    await client.SendPacketAsync(new ClientIDResponsePacket(false));
                    return;
                }

                await client.SendPacketAsync(new ClientIDResponsePacket(true));

                PropertyInfo[] props = this.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(FieldDeviceAttribute)) != null & x.PropertyType.IsSubclassOf(typeof(BaseFieldDevice))).ToArray();
                foreach (PropertyInfo info in props)
                {
                    var attr = info.GetCustomAttribute<FieldDeviceAttribute>();
                    if (attr.deviceType != packet.DeviceType || attr.teamColor != packet.TeamColor) continue;

                    object? obj = Activator.CreateInstance(info.PropertyType);
                    if (obj == null)
                    {
                        Logger.Log(LogLevel.WARNING, "Failed to create instance of field device");
                        return;
                    }
                    BaseFieldDevice fieldDevice = (BaseFieldDevice)obj;
                    fieldDevice.Initialize(client, attr.deviceType, attr.pointSource, attr.teamColor);
                    fieldDevice.OnPingExpire += HandlePingExpire;

                    info.SetValue(this, obj);
                    Logger.Log($"Field Device Connected ({fieldDevice.Name}) [{fieldDevice.TCPClient.GetHashCode()}]");

                    return;
                }


                Logger.Log(LogLevel.WARNING, $"Couldn't find the property for the device {packet.DeviceType} (Team: {packet.TeamColor})");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while creating field device (Ex: {ex.Message})");
            }
        }
    }


    internal class FieldDeviceAttribute : Attribute
    {
        public DeviceType deviceType { get; private set; }
        public TeamColor teamColor { get; private set; }

        public PointSource pointSource { get; private set; }

        public FieldDeviceAttribute(DeviceType _deviceType, PointSource _pointSource, TeamColor _team)
        {
            deviceType = _deviceType;
            teamColor = _team;
            pointSource = _pointSource;
        }
    }

}
