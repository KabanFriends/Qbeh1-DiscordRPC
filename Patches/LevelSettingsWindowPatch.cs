using HarmonyLib;
using UnityEngine;

namespace Qbeh1_DiscordRPC.Patches
{
    public static class LevelSettingsWindowPatch
    {
        [HarmonyPatch(typeof(LevelSettingsWindow), "DoWindow")]
        [HarmonyPostfix]
        private static void DoWindowPatch(LevelSettingsWindow __instance)
        {
            if (!cInput.scanning && !(bool) Traverse.Create(__instance).Field("wasScanning").GetValue() && (GUILayout.Button("Close", new GUILayoutOption[] { GUILayout.Width(80f) }) || (Event.current.isKey && Event.current.keyCode == KeyCode.Escape && Event.current.type == EventType.KeyUp)))
            {
                DiscordHandler.UpdatePresence("Editing a level:", "\"" + Qbeditor.currentLevelName + "\"");
            }
        }
    }
}
