using BepInEx.Bootstrap;
using BepInEx.Logging;
using HarmonyLib;

namespace BepInEx.Core.Cobweb;

internal class UtilsPatches
{
}

/*[HarmonyPatch("VersionNumberTextMesh", "Start")]
internal class CobwebPatch_VersionNumber
{
    [HarmonyPrefix]
    public static bool SkipIfModded(VersionNumberTextMesh __instance)
    {
        if (Util.plugin_status == "after")
        {
            foreach (var keyValuePair in Util.Plugins)
            {
                __instance.textMesh.text += $"\n<color=red>{keyValuePair.Value.TypeName}</color>";
            }

            return false;
        }

        return true;
    }
}*/
