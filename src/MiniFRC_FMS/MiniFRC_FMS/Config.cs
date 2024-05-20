using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS
{
    internal static class Config
    {
        public const string WebSVRootURL = "http://localhost:8080/";
        public const string WebSocketRootURL = "ws://10.134.100.230:8082";
        public const string TCPServerEndpoint = "10.134.100.230:8081";
        public const int PingExpireTimeMS = 5000;

        public const ulong SecurityKey = 16777216;

        public static class Field
        {
            public const int SpeakerScore = 2;
            public const int SpeakerAmplifiedScore = 5;

            public const int AmplificationDuration = 20000;
            public const int AmpScore = 1;

            public const int StageClimbPerRobot = 2;
            public const int Trap = 5;

            public const int SourceCooldown = 10;


            public const int PenaltyPoints = 5;
        }
    }
}
