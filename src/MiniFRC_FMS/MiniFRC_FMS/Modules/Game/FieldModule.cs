using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.FieldItems;
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
        [FieldDevice(nameof(REDSpeaker), DeviceType.Speaker, PointSource.Speaker, TeamColor.RED)]
        public Speaker? REDSpeaker { get; private set; } = null;


        [FieldDevice(nameof(REDSpeaker), DeviceType.Speaker, PointSource.Speaker, TeamColor.BLUE)]
        public Speaker? BLUESpeaker { get; private set; } = null;



        protected override bool Init()
        {
            GetModule<TCPServerModule>()?.AttachPacketCallback<ClientIDPacket>(HandleClientIdentification);
            return true;
        }

        void HandlePingExpire(object? sender, BaseFieldDevice fieldDevice)
        {
            Logger.Log(LogLevel.WARNING, $"Potential disconnection from \"{fieldDevice.Nickname}\"");
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

                PropertyInfo[] props = this.GetType().GetProperties().Where(x => x.GetCustomAttribute(typeof(FieldDeviceAttribute)) != null).ToArray();
                foreach (PropertyInfo info in props)
                {
                    var attr = info.GetCustomAttribute<FieldDeviceAttribute>();
                    if (attr.deviceType != packet.DeviceType || attr.teamColor != packet.TeamColor) continue;

                    object? obj = Activator.CreateInstance(info.PropertyType, client, attr.Nickname);
                    if (obj == null)
                    {
                        Logger.Log(LogLevel.WARNING, "Failed to create instance of field item");
                        return;
                    }
                    BaseFieldDevice fieldDevice = (BaseFieldDevice)obj;

                    info.SetValue(this, obj);
                    Logger.Log($"Field Item Connected ({fieldDevice.Nickname})");

                    return;
                }

                Logger.Log(LogLevel.WARNING, $"Couldn't find the property for the device {packet.DeviceType} (Team: {packet.TeamColor})");
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.ERROR, $"An error occured while creating field item (Ex: {ex.Message})");
            }
            
            /*
            switch (packet.DeviceType)
            {

                case Models.DeviceType.Speaker:
                    if (packet.TeamColor == TeamColor.RED)
                    {
                        REDSpeaker = new Speaker(client, "REDSpeaker");
                        REDSpeaker.ScoreCB = () => OnSpeakerScore?.Invoke(null, TeamColor.RED);
                        REDSpeaker.OnPingExpire += HandlePingExpire;
                        Logger.Log("FIELD: RED Speaker Connected");
                    }
                    else if (packet.TeamColor == TeamColor.BLUE)
                    {
                        BLUESpeaker = new Speaker(client, "BLUE Speaker");
                        BLUESpeaker.ScoreCB = () => OnSpeakerScore?.Invoke(null, TeamColor.BLUE);
                        Logger.Log("FIELD: BLUE Speaker Connected");
                    }
                break;
            }

            */
        }
    }


    internal class FieldDeviceAttribute : Attribute
    {
        public string Nickname { get; private set; }
        public DeviceType deviceType { get; private set; }
        public TeamColor teamColor { get; private set; }

        public PointSource pointSource { get; private set; }

        public FieldDeviceAttribute(string nickname, DeviceType _deviceType, PointSource _pointSource, TeamColor _team)
        {
            Nickname = nickname;
            deviceType = _deviceType;
            teamColor = _team;
            pointSource = _pointSource;
        }
    }

}
