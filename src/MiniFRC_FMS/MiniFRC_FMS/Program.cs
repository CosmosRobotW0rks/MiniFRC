using MiniFRC_FMS.Modules.Game.Models;
using MiniFRC_FMS.Modules;
using MiniFRC_FMS.Utils;
using System.Reflection;
using MiniFRC_FMS.Modules.Game;

namespace MiniFRC_FMS
{

    internal class Program
    {

        static async Task Main(string[] args)
        {
            Logger.Log(LogLevel.INFO, "Starting MiniFRC FMS");

            ModulesMain.Init();

            //ModulesMain.Instance.GetModule<FieldModule>().OnSpeakerScore += FieldModule_OnSpeakerScore;

            await Task.Delay(-1);
        }

    }
}
