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
    
    [HarmonyPatch("HudController", "ShowQuickGamePrompt")]
    internal class CobwebPatch_QuickGame2
    {
        [HarmonyPrefix]
        public static bool SkipIfModded()
        {
            Logger.Log(LogLevel.Info, "Attempted to show QuickPlay prompt. BLOCKED!");
            return false;
        }
    }

    [HarmonyPatch("VersionNumberTextMesh", "Start")]
    internal class CobwebPatch_VersionText
    {
        private static bool hasDoneTextPatch = false;

        [HarmonyPostfix]
        public static void AddText(ref object __instance)
        {
            if(!hasDoneTextPatch)
            {
                var textMeshInfo = __instance.GetType().GetField("textMesh");
                var textMesh = textMeshInfo.GetValue(__instance);

                //Logger.Log(LogLevel.Warning, "TextMesh via reflecttion: " + textMesh.ToString());

                var setTextInfo = textMesh.GetType().GetMethods().Where(m => m.Name == "SetText").Where(m => m.GetParameters().Length == 1).First();

                var currentText = (string)textMesh.GetType().GetProperty("text").GetValue(textMesh, null);
                StringBuilder sb = new StringBuilder(currentText);
                sb.Append("\n\nMods:");

                foreach (var plugin in Util.Plugins)
                {
                    var name = plugin.Value.Metadata.Name;
                    var version = plugin.Value.Metadata.Version;

                    sb.Append("\n- ");
                    sb.Append(name);
                    sb.Append(" v");
                    sb.Append(version);
                }

                setTextInfo.Invoke(textMesh, new object[] { sb });

                textMeshInfo.SetValue(__instance, textMesh);

                hasDoneTextPatch = true;
            }
        }
    }
}
