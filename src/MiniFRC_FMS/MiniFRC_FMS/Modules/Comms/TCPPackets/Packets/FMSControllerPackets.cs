using MiniFRC_FMS.Modules.Game.Models;
using PacketCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.Comms.TCPPackets.Packets  // 20-69
{

    #region AUTH
    internal struct FMSControllerAuthPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerAuthPacket;

        public ulong SecurityKey { get; set; }
    }

    internal struct FMSControllerAuthResponsePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerAuthResponsePacket;

        public byte Authenticated { get; set; }

        public FMSControllerAuthResponsePacket(bool authenticated)
        {
            Authenticated = authenticated ? (byte)1 : (byte)0;
        }
        public FMSControllerAuthResponsePacket() { }
    }
    #endregion

    #region MATCH CONTROL
    internal struct FMSControllerLoadMatchPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerLoadMatchPacket;

        public byte ID_RED1 { get; set; }
        public byte ID_RED2 { get; set; }
        public byte ID_RED3 { get; set; }
        public byte ID_BLUE1 { get; set; }
        public byte ID_BLUE2 { get; set; }
        public byte ID_BLUE3 { get; set; }

        public byte MatchID { get; set; }
        public ushort MatchDuration { get; set; }

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
        public byte ID => (byte)PacketIDs.FMSControllerLoadMatchResponsePacket;

        public MatchLoadStatus matchLoadStatus { get; set; }

        public enum MatchLoadStatus : byte
        {
            Success = 0,
            IncorrectTeamIDs,
            MatchExists,
            IncorrectMatchState,
            SomethingElseWentWrong,
        }

        public FMSControllerLoadMatchResponsePacket(MatchLoadStatus status)
        {
            matchLoadStatus = status;
        }
        public FMSControllerLoadMatchResponsePacket() { }
    }

    internal struct FMSControllerStartStopMatchPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerStartStopMatchPacket;

        public byte State { get; set; }

        public FMSControllerStartStopMatchPacket(bool start)
        {
            State = start ? (byte)1 : (byte)0;
        }
        public FMSControllerStartStopMatchPacket() { }
    }

    internal struct FMSControllerStartStopMatchResponsePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerStartStopMatchResponsePacket;

        public byte Success { get; set; }

        public FMSControllerStartStopMatchResponsePacket(bool success)
        {
            Success = success ? (byte)1 : (byte)0;
        }
        public FMSControllerStartStopMatchResponsePacket() { }
    }

    internal struct FMSControllerMatchStateUpdatedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerMatchStateUpdatedPacket;

        public MatchState matchState { get; set; }

        public int ID_RED1 { get; set; }
        public int ID_RED2 { get; set; }
        public int ID_RED3 { get; set; }
        public int ID_BLUE1 { get; set; }
        public int ID_BLUE2 { get; set; }
        public int ID_BLUE3 { get; set; }

        public int MatchID { get; set; }
        public ushort MatchDuration { get; set; }
        public ushort RemainingTime { get; set; }
        public byte Countdown { get; set; }


        public byte Rematch { get; set; }
        public byte Practice { get; set; }

        public int REDPoints { get; set; }
        public int BLUEPoints { get; set; }

        public FMSControllerLoadMatchPacket.MatchType matchType { get; set; }

        public enum MatchState : byte
        {
            Standby = 0,
            Loaded,
            Countdown,
            Running,
            PointsCalculating,
            AfterMatch
        }
    }
    #endregion

    #region POINT CONTROL
    internal struct FMSControllerPointAddedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerNewPointAddedPacket;

        public int PointID;
        public TeamColor Alliance;
        public PointSource PointSource;
        public int PointValue;

        public FMSControllerPointAddedPacket(int pointID, TeamColor alliance, PointSource pointSource, int pointValue)
        {
            PointID = pointID;
            Alliance = alliance;
            PointSource = pointSource;
            PointValue = pointValue;
        }
    }

    internal struct FMSControllerPointRemovedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerPointRemovedPacket;

        public TeamColor Alliance;

        public int PointID;

        public FMSControllerPointRemovedPacket(int pointID, TeamColor alliance)
        {
            this.PointID = pointID;
            this.Alliance = alliance;
        }
    }

    internal struct FMSControllerRemovePointPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerRemovePointPacket;

        public TeamColor Alliance;

        public int PointID;

        public FMSControllerRemovePointPacket(int pointID, TeamColor alliance)
        {
            this.PointID = pointID;
            this.Alliance = alliance;
        }
    }

    internal struct FMSControllerApprovePointsPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerApprovePointsPacket;
    }

    #endregion

    #region AUDIENCE DISPLAY
    // AuDis = Audience Display

    internal struct FMSControllerAuDisPageUpdatedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerAuDisPageUpdatedPacket;

        public AuDisPage auDisPage { get; set; }
    }

    internal struct FMSControllerSwitchAuDisPagePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerSwitchAuDisPagePacket;

        public AuDisPage auDisPage { get; set; }

    }
    #endregion

    #region DEVICE INFO
    internal struct FMSControllerDeviceLastseenUpdatedPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerDeviceLastseenUpdatedPacket;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] deviceIDs = new byte[100];

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public long[] deviceLastSeens = new long[100];

        public byte devicesLength = 0;

        public FMSControllerDeviceLastseenUpdatedPacket(Dictionary<(DeviceType, TeamColor), DateTime> _deviceIds)
        {
            deviceIDs = new byte[100];
            deviceLastSeens = new long[100];


            for (int i = 0; i < _deviceIds.Count; i++)
            {
                var deviceId = _deviceIds.ElementAt(i).Key;
                deviceIDs[i] = (byte)((byte)deviceId.Item1 | (byte)deviceId.Item2 << 6);
                deviceLastSeens[i] = _deviceIds.ElementAt(i).Value.Ticks;
            }

            devicesLength = (byte)_deviceIds.Count;
        }
        public FMSControllerDeviceLastseenUpdatedPacket() { }

        public Dictionary<(DeviceType, TeamColor), DateTime> GetDevices()
        {
            Dictionary<(DeviceType, TeamColor), DateTime> devices = new Dictionary<(DeviceType, TeamColor), DateTime>();

            for (int i = 0; i < devicesLength; i++)
            {
                DeviceType deviceType = (DeviceType)(deviceIDs[i] & 0b00111111);
                TeamColor teamColor = (TeamColor)((deviceIDs[i] & 0b11000000) >> 6);

                devices.Add((deviceType, teamColor), new DateTime(deviceLastSeens[i]));
            }

            return devices;
        }
    }
    #endregion

    #region DEVICE CONTROL

    internal struct FMSControllerEnableDisableDevicePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerEnableDisableDevicePacket;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] DeviceIDs = new byte[100];

        public int DeviceCount { get; set; }
        public bool Enabled { get; set; }

        public FMSControllerEnableDisableDevicePacket() { }

        public FMSControllerEnableDisableDevicePacket(byte[] deviceIDs, bool enabled)
        {
            deviceIDs.CopyTo(DeviceIDs, 0);

            this.DeviceCount = deviceIDs.Length;

            Enabled = enabled;
        }

    }

    internal struct FMSControllerEnableDisableDeviceResponsePacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerEnableDisableDeviceResponsePacket;
    }
    #endregion


    #region FIELD CONTROL



    internal struct FMSControllerToggleElectricityPacket : IBasePacket
    {
        public byte ID => (byte)PacketIDs.FMSControllerToggleElectricityPacket;


        public byte State { get; set; }

        public FMSControllerToggleElectricityPacket(bool state)
        {
            State = state ? (byte)1 : (byte)0;
        }
    }

    #endregion


}