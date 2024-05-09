using MiniFRC_FMS.Modules.Comms.TCPPackets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldItems
{
    internal class Speaker : BaseFieldDevice
    {
        public Action ScoreCB { get; set; }

        public override void Init()
        {

        }

    }
}
