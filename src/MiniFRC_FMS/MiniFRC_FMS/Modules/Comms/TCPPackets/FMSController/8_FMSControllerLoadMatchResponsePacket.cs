using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.FMSController
{
    internal struct FMSControllerLoadMatchResponsePacket : IBasePacket
    {
        public byte ID => 8;

        public MatchLoadStatus matchLoadStatus { get; set; }

        public enum MatchLoadStatus : byte
        {
            Success = 0,
            SomethingElseWentWrong,
            IncorrectTeamIDs
        }

        public FMSControllerLoadMatchResponsePacket(MatchLoadStatus status)
        {
            matchLoadStatus = status;
        }
    }
}
