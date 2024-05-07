using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldItemPackets;
using MiniFRC_FMS.Modules.Game.FieldItems;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game
{
    internal class FieldModule : BaseModule
    {
        private Speaker? REDSpeaker = null;
        private Speaker? BLUESpeaker = null;

        public event EventHandler<TeamColor> OnSpeakerScore;


        protected override bool Init()
        {
            GetModule<TCPServerModule>()?.AttachPacketCallback<ClientIDPacket>(HandleClientIdentification);
            return true;
        }

        void HandlePingExpire(object? sender, BaseFieldItem fieldItem)
        {
            Logger.Log(LogLevel.WARNING, $"Potential disconnection from \"{fieldItem.Nickname}\"");
        }

        async void HandleClientIdentification(Client client, ClientIDPacket packet)
        {
            if (packet.SecurityKey != Config.SecurityKey)
            {
                await client.SendPacketAsync(new ClientIDResponsePacket(false));
                return;
            }
            await client.SendPacketAsync(new ClientIDResponsePacket(true));

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


        }
    }
}
