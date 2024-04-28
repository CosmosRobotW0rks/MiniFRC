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
        public const string TCPServerEndpoint = "192.168.1.34:8081";

        public const ulong SecurityKey = 16777216;

        public static class Field
        {
            public const string REDSpeakerHost = "";
            public const int REDSpeakerPort = 0;

            public const string BLUESpeakerHost = "";
            public const int BLUESpeakerPort = 0;
        }

        public static class Points
        {
            public const int SpeakerScore = 5;
        }
    }
}
