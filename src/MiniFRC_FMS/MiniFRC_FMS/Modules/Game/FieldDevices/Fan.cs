using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{

    internal class Fan : BaseFieldDevice
    {
        public override void Init()
        {
            ToggleElectricity(true).Wait();
        }

        public async Task ToggleElectricity(bool state)
        {
            try
            {
                await this.TCPClient.SendPacketAsync(new FanToggleElectricityPacket(state));
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.WARNING, $"Failed to toggle field electricity: {ex.Message}");
            }

        }
    }
}