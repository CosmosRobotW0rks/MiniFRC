using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp.Comms.Packets
{
    internal struct FMSControllerLoadMatchPacket : IBasePacket
    {
        public byte ID => 7;

        public byte ID_RED1 { get; set; }
        public byte ID_RED2 { get; set; }
        public byte ID_BLUE1 { get; set; }
        public byte ID_BLUE2 { get; set; }

        public byte MatchID { get; set; }
        public UInt16 MatchDuration { get; set; }

        public byte Rematch { get; set; }
        public byte Practice { get; set; }
        
        public MatchType matchType { get; set; }


        public enum MatchType : byte
        {
            Qualification = 0,
            Semifinal = 1,
            Final = 2
        }
    }
}
