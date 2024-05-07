using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp.Comms
{
    internal struct FMSControllerAuthPacket : IBasePacket
    {
        public byte ID => 5;

        public ulong SecurityKey { get; set; }
    }

    internal struct FMSControllerAuthResponsePacket : IBasePacket
    {
        public byte ID => 6;

        public byte Authenticated { get; set; }

        public FMSControllerAuthResponsePacket(bool authenticated)
        {
            Authenticated = authenticated ? (byte)1 : (byte)0;
        }
        public FMSControllerAuthResponsePacket() { }
    }

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
        public FMSControllerLoadMatchResponsePacket() { }
    }

    internal struct FMSControllerStartStopMatchPacket : IBasePacket
    {
        public byte ID => 9;

        public byte State { get; set; }

        public FMSControllerStartStopMatchPacket(bool start)
        {
            State = start ? (byte)1 : (byte)0;
        }
        public FMSControllerStartStopMatchPacket() { }
    }

    internal struct FMSControllerStartStopMatchResponsePacket : IBasePacket
    {
        public byte ID => 10;

        public byte Success { get; set; }

        public FMSControllerStartStopMatchResponsePacket(bool success)
        {
            Success = success ? (byte)1 : (byte)0;
        }
        public FMSControllerStartStopMatchResponsePacket() { }
    }
}
