﻿using MiniFRC_FMS.Modules.Game.Models;
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

            ModulesMain.InitModules();


            FieldModule.OnSpeakerScore += FieldModule_OnSpeakerScore;

            await Task.Delay(-1);
        }

        private static void FieldModule_OnSpeakerScore(object? sender, TeamColor e)
        {
            Logger.Log(LogLevel.INFO, $"Speaker Scored for {e}");
        }
    }
}
