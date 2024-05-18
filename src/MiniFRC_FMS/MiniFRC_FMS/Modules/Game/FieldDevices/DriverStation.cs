using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
using PacketCommunication.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Game.FieldDevices
{
    internal class DriverStation : BaseFieldDevice
    {
        public override void Init()
        {
            TCPClient.AttachPacketCallback<DriverStationAmpPressedPacket>(HandleAmpBtn);
            TCPClient.AttachPacketCallback<DriverStationSourcePressedPacket>(HandleSourceBtn);
        }

        public override void MatchStart()
        {
            NotifyReadyToTriggerSourceAsync().Wait();
        }

        private void HandleAmpBtn(DriverStationAmpPressedPacket p)
        {
            Speaker? spkr = ModulesMain.Instance.GetModule<FieldModule>().GetFieldDevice<Speaker>(teamColor);
            if (spkr == null) { Logger.Log(LogLevel.WARNING, "Speaker is null, cannot amplify"); return; }

            if (spkr.ReadyToBeAmplified && ModulesMain.Instance.GetModule<MatchModule>().State == FMSControllerMatchStateUpdatedPacket.MatchState.Running)
            {
                spkr.AmplifyAsync().Wait();
                TCPClient.SendPacketAsync(new DriverStationAmplifiedPacket());
            }
        }

        private void HandleSourceBtn(DriverStationSourcePressedPacket p)
        {
            Source? src = ModulesMain.Instance.GetModule<FieldModule>().GetFieldDevice<Source>(teamColor);
            if (src == null) { Logger.Log(LogLevel.WARNING, "Source is null, cannot trigger"); return; }
            if (src.NextNoteDropTime > DateTime.Now) return;
            if(ModulesMain.Instance.GetModule<MatchModule>().State != FMSControllerMatchStateUpdatedPacket.MatchState.Running) return;

            src.DropAsync().Wait();
            TCPClient.SendPacketAsync(new DriverStationSourceTriggeredPacket());
        }

        public async Task NotifyReadyToAmplifyAsync()
        {
            await TCPClient.SendPacketAsync(new DriverStationAmpReadyPacket());
        }

        public async Task NotifyReadyToTriggerSourceAsync()
        {
            await TCPClient.SendPacketAsync(new DriverStationSourceReadyPacket());
        }
    }
}
