using MiniFRC_FMS.FieldItems;
using MiniFRC_FMS.Modules;
using MiniFRC_FMS.Utils;
using System.Reflection;

namespace MiniFRC_FMS
{

    internal class Program
    {
        static async Task Main(string[] args)
        {
            Logger.Log(LogLevel.INFO, "Starting MiniFRC FMS");

            ModulesMain.InitModules();

            await Task.Delay(-1);
        }


    }
}
