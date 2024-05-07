using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules.DataSaving
{
    internal static class Paths
    {
        public const string BasePath = "MiniFRC_FMS_Data";

        public static string TeamsFilePath => Path.Combine(BasePath, "teams.json");
        public static string MatchesFilePath => Path.Combine(BasePath, "matches.json");
    }
}
