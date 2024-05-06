using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp.Comms.Packets
{
    internal struct FMSControllerLoadMatchResponsePacket : IBasePacket
    {
        public byte ID => 8;

        MatchLoadStatus matchLoadStatus { get; set; }

        public enum MatchLoadStatus : byte
        {
            Success = 0,
            SomethingElseWentWrong,
            IncorrectTeamIDs
        }
    }
}
