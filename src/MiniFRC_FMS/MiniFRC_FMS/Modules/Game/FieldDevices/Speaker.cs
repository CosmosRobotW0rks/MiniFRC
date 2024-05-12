using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication.Client;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{
    internal class Speaker : BaseFieldDevice
    {
        public Action ScoreCB { get; set; }

        public override void Init()
        {
            this.TCPClient.AttachPacketCallback<SpeakerScorePacket>(x =>
            {
                this.Score(5);
            });
        }

    }
}
