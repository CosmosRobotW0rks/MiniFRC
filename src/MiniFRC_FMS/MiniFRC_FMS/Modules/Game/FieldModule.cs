using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.FieldDevices;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication;
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
        [FieldDevice(DeviceType.Fan, PointSource.Other, TeamColor.NONE)]
        public Fan? Fan { get; private set; } = null;

        [FieldDevice(DeviceType.Speaker, PointSource.Speaker, TeamColor.RED)]
        public Speaker? REDSpeaker { get; private set; } = null;


        [FieldDevice(DeviceType.Speaker, PointSource.Speaker, TeamColor.BLUE)]
        public Speaker? BLUESpeaker { get; private set; } = null;


        [FieldDevice(DeviceType.Source, PointSource.Other, TeamColor.RED)]
        public Source? REDSource { get; private set; } = null;

        [FieldDevice(DeviceType.Source, PointSource.Other, TeamColor.BLUE)]
        public Source? BLUESource { get; private set; } = null;

        [FieldDevice(DeviceType.Amp, PointSource.Amp, TeamColor.RED)]
        public Amp? REDAmp { get; private set; } = null;

        [FieldDevice(DeviceType.Amp, PointSource.Amp, TeamColor.BLUE)]
        public Amp? BLUEAmp { get; private set; } = null;

        [FieldDevice(DeviceType.DriverStation, PointSource.Other, TeamColor.RED)]
        public DriverStation? REDDriverStation { get; private set; } = null;

        [FieldDevice(DeviceType.DriverStation, PointSource.Other, TeamColor.BLUE)]
        public DriverStation? BLUEDriverStation { get; private set; } = null;


        public BaseFieldDevice[] GetAllFieldDevices(Func<BaseFieldDevice, bool>? condition = null)
        {
            PropertyInfo[] props = this.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(FieldDeviceAttribute)) != null && x.PropertyType.IsSubclassOf(typeof(BaseFieldDevice))).ToArray();
            List<BaseFieldDevice> devices = new();

            foreach (PropertyInfo info in props)
            {
                BaseFieldDevice? device = (BaseFieldDevice?)info.GetValue(this);
                if (device == null) continue;

                devices.Add(device);
            }

            return condition == null ? devices.ToArray() : devices.Where(condition).ToArray();
        }

        public T? GetFieldDevice<T>(TeamColor team) where T : BaseFieldDevice
        {
            BaseFieldDevice[] devices = GetAllFieldDevices();

            return (T?)devices.Where(y => y.GetType() == typeof(T) && y.teamColor == team).FirstOrDefault();
        }

        private async Task AnnouncePacketAsync<T>(T packet) where T : IBasePacket, new()
        {
            try
            {
                BaseFieldDevice[] devices = GetAllFieldDevices();

                List<Task> tasks = new();

                foreach (BaseFieldDevice device in devices)
                {
                    if (device.TCPClient == null)
                    {
                        Logger.Log(LogLevel.WARNING, $"Device {device.Name} TCPClient is null");
                        continue;
                    }

                    tasks.Add(device.TCPClient.SendPacketAsync(packet));
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while announcing packet to field devices (Ex: {ex.Message})");
            }
        }

        public async Task ToggleEnabledAllAsync(bool enabled)
        {
            try
            {
                BaseFieldDevice[] devices = GetAllFieldDevices();

                List<Task> t = new();
                foreach (BaseFieldDevice device in devices)
                {

                    t.Add(device.SetEnabledAsync(enabled));
                }

                await Task.WhenAll(t);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while toggling enabled states (Ex: {ex.Message})");
            }
        }

        public async Task AnnounceMatchStartStopAsync(bool state)
        {
            try
            {

                BaseFieldDevice[] devices = GetAllFieldDevices();

                foreach (BaseFieldDevice device in devices)
                {
                    if (state)
                        device.MatchStart();
                    else
                        device.MatchStop();
                }
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while announcing match enabled/disabled (Ex: {ex.Message})");
            }
        }


        protected override bool Init()
        {
            var tcpServerModule = GetModule<TCPServerModule>();
            tcpServerModule.AttachPacketCallback<ClientIDPacket>(HandleClientIdentification);
            tcpServerModule.AttachPacketCallback<ClientInitializationStatusPacket>(HandleClientInitStatus);

            Task.Run(UpdateFMSControllersWithFieldDataAsync);
            return true;
        }



        private async Task UpdateFMSControllersWithFieldDataAsync()
        {
            FMSControllerAppModule fmsControllerModule = GetModule<FMSControllerAppModule>();
            while (true)
            {
                try
                {

                    fmsControllerModule.AnnounceDeviceStatesAsync(GetDevicesInfo());
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


        async void HandleClientInitStatus(Client client, ClientInitializationStatusPacket packet)
        {
            BaseFieldDevice? device = GetAllFieldDevices(x => x.TCPClient == client).FirstOrDefault();
            if (device == null) return;

            if (packet.Initialized == 1) Logger.Log($"{device.Name} initialized");
            else Logger.Log(LogLevel.WARNING, $"{device.Name} failed to initialize");
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
