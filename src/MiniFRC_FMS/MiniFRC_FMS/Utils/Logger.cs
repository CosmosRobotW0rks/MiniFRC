using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniFRC_FMS.Utils
{
    internal enum LogLevel
    {
        DEBUG,
        INFO,
        WARNING,
        ERROR
    };

    internal static class Logger
    {

        private static Dictionary<LogLevel, int> priorityColors = new Dictionary<LogLevel, int>()
        {
            { LogLevel.DEBUG, 13},
            { LogLevel.INFO, 3},
            { LogLevel.WARNING, 6},
            { LogLevel.ERROR, 12}
        };


        public static void Log(string text, LogLevel priority = LogLevel.INFO)
        {
            string textToPrint = $"[{DateTime.Now}] `{priorityColors[priority]}`[{priority}] `15`{text}";

            ColoredConsole.WriteLine(textToPrint);
        }

        public static void Log(LogLevel priority, string text)
        {
            Log(text, priority);
        }
    }
}
