using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BepInEx.Core.Cobweb
{
    internal class MultiplayerPatches
    {
    }

    [HarmonyPatch("SteamLeaderboards", "UpdateScore")]
    internal class CobwebPatch_Leaderboard
    {
        [HarmonyPrefix]
        public static bool SkipIfModded(int score)
        {
            Logger.Log(LogLevel.Info, "Skipping leaderboard save of " + score);
            return false;
        }
    }

    /*[HarmonyPatch("QuickGameHud", "Awake")]
    internal class CobwebPatch_QuickGame
    {
        [HarmonyPrefix]
        public static bool SkipIfModded()
        {
            Logger.Log(LogLevel.Info, "Removing game types from Quick Play");
            return false;
        }
    }

    /*[HarmonyPatch("QuickGameHud", "GetGameType")]
    internal class CobwebPatch_QuickGame2
    {
        [HarmonyPrefix]
        public static bool SkipIfModded(ref string __result)
        {
            Logger.Log(LogLevel.Info, "Removing game types from Quick Play");
            __result = "Multiplayer has been disabled to prevent cheating.";
            return false;
        }
    }*/
    
    [HarmonyPatch("HudController", "LoginToEpic")]
    internal class CobwebPatch_QuickGame2
    {
        [HarmonyPrefix]
        public static bool SkipIfModded()
        {
            return false;
        }
    }
}
