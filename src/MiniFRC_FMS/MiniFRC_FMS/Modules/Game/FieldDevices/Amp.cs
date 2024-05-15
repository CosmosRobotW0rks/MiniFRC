using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{
    internal class Amp : BaseFieldDevice
    {
        public override void Init()
        {
            this.TCPClient.AttachPacketCallback<AmpScorePacket>(x =>
            {
                this.Score(5);
            });
        }

    }
}
