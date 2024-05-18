using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{
    internal class Source : BaseFieldDevice
    {
        public DateTime NextNoteDropTime { get; private set; } = DateTime.MinValue;

        public override void Init()
        {

        }

        public async Task DropAsync()
        {
            await TCPClient.SendPacketAsync(new SourceDropPacket());
        }

    }
}
