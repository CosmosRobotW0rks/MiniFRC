using MiniFRC_FMS.FieldItems;
using MiniFRC_FMS.Models;
using SimpleWebServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Modules
{
    [ModuleInitPriority(2)]
    internal static class MatchModule
    {
        private static Team? REDTeam;
        private static Team? BLUETeam;

        private static Speaker? REDSpeaker;
        private static Speaker? BLUESpeaker;


        public static bool Initialize()
        {
            REDSpeaker = new Speaker(Config.Field.REDSpeakerHost, Config.Field.REDSpeakerPort, TeamColor.RED);
            BLUESpeaker = new Speaker(Config.Field.BLUESpeakerHost, Config.Field.BLUESpeakerPort, TeamColor.BLUE);

            REDSpeaker.NoteScored += REDSpeaker_NoteScored;
            BLUESpeaker.NoteScored += BLUESpeaker_NoteScored;
            return true;
        }

        private static void BLUESpeaker_NoteScored()
        {
            Console.WriteLine("Blue speaker note scored");
        }

        private static void REDSpeaker_NoteScored()
        {
            Console.WriteLine("Red speaker note scored");
        }
    }
}
