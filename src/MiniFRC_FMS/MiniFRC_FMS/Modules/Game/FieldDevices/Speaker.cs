using MiniFRC_FMS.Modules.Comms;
using MiniFRC_FMS.Modules.Comms.TCPPackets.Packets.FieldDevicePackets;
using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Utils;
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

        public bool ReadyToBeAmplified { get; private set; } = false;

        public bool Amplified => AmplifiedUntil > DateTime.Now;

        public DateTime AmplifiedUntil { get; private set; } = DateTime.MinValue;


        public override void Init()
        {
            this.TCPClient.AttachPacketCallback<SpeakerScorePacket>(x =>
            {
                if (Amplified) this.Score(Config.Field.SpeakerAmplifiedScore);
                else this.Score(Config.Field.SpeakerScore);
            });

        }

        public async Task AmplifyAsync()
        {
            AmplifiedUntil = DateTime.Now.AddMilliseconds(Config.Field.AmplificationDuration);

            ReadyToBeAmplified = false;
            _ = Task.Run(async () =>
            {
                while (AmplifiedUntil > DateTime.Now) await Task.Delay(10);

                Logger.Log("Not Amplified Anymore", LogLevel.DEBUG);

                var amp = (Amp)ModulesMain.Instance.GetModule<FieldModule>().GetAllFieldDevices(x => x.teamColor == this.teamColor && x.deviceType == DeviceType.Amp).FirstOrDefault();
                if (amp == null) { Logger.Log("Amp is null, cannot send amplified notify packet"); return; }
                await amp.NotifyAmplificationEndedAsync();
            });

            Logger.Log(LogLevel.DEBUG, "AMPLIFIED");

        }

        public async Task SetReadyToAmplifyAsync()
        {
            ReadyToBeAmplified = true;

            DriverStation? ds = ModulesMain.Instance.GetModule<FieldModule>().GetAllFieldDevices(x => x.teamColor == this.teamColor && x.deviceType == DeviceType.DriverStation).Cast<DriverStation>().FirstOrDefault();
            if(ds == null) Logger.Log("DriverStation is null, cannot send ready to amplify packet");
            else await ds.NotifyReadyToAmplifyAsync();
        }

        public override void MatchStop()
        {
            ReadyToBeAmplified = false;
            AmplifiedUntil = DateTime.MinValue;
        }
    }
}
