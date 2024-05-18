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

            while(true)
            {
                try
                {
                    string str = Console.ReadLine();
                    switch (str)
                    {
                        case "amplify":
                            await ModulesMain.Instance.GetModule<FieldModule>().REDSpeaker.SetReadyToAmplifyAsync();
                            Logger.Log(LogLevel.DEBUG, "Amplified amp");
                            break;
                        case "updateleaderboard":
                            ModulesMain.Instance.GetModule<AuDisModule>().UpdateLeaderboard();
                            break;

                        default:
                            int input = int.Parse(str);


                            ModulesMain.Instance.GetModule<AuDisModule>().SwitchPage((AuDisPage)input);
                            break;
                    }
                }
                catch(Exception ex)
                {

                }
                
            }
        }

    }
}
