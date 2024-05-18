using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_ControlApp
{
    internal static class Utils
    {
        public static byte GetDeviceIDByDeviceInfo(DeviceType deviceType, TeamColor teamColor)
        {
            return (byte)(((byte)teamColor<<6) | (byte)deviceType);
        }

        public static string GetDeviceNameByDeviceInfo(DeviceType deviceType, TeamColor teamColor)
        {
            string teamColStr = teamColor == TeamColor.NONE ? "" : teamColor.ToString() + " ";
            string res = $"{teamColStr}{deviceType}";
            return res;
        }
    }
}
