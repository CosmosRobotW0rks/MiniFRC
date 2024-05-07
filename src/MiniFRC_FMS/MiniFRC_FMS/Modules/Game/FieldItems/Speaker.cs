using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldItemPackets;
using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldItems
{
    internal class Speaker : BaseFieldItem
    {
        public Action ScoreCB { get; set; }


        public Speaker(Client client, string nickname) : base(client, nickname)
        {
            client.AttachPacketCallback<SpeakerScorePacket>((packet) =>
            {
                ScoreCB?.Invoke();
            });
            
        }

    }
}
