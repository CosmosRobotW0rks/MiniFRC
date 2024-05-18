using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.Models;
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
                var field = ModulesMain.Instance.GetModule<FieldModule>();

                Speaker? speaker = (Speaker)field.GetAllFieldDevices(x => x.deviceType == DeviceType.Speaker && x.teamColor == this.teamColor).FirstOrDefault();
                if(speaker == null) { Logger.Log(LogLevel.WARNING, "Speaker is null, cannot set ready to amplify"); return; }
                
                speaker.SetReadyToAmplifyAsync().Wait();

                this.Score(Config.Field.AmpScore);
            });
        }

        public async Task NotifyAmplificationEndedAsync()
        {
            await this.TCPClient.SendPacketAsync(new AmpAmplifiedPacket());
        }

    }
}
