using HarmonyLib;

namespace Qbeh1_DiscordRPC.Patches
{
    public class OpenLevelWindowPatch
    {
        [HarmonyPatch(typeof(OpenLevelWindow), "Start")]
        [HarmonyPostfix]
        private static void StartPatch()
        {
            DiscordHandler.UpdatePresence("Editor Main Menu", null);
        }

        [HarmonyPatch(typeof(OpenLevelWindow), "OpenSelectedLevel")]
        [HarmonyPostfix]
        private static void OpenLevelPatch()
        {
            DiscordHandler.UpdatePresence("Editing a level:", "\"" + Qbeditor.currentLevelName + "\"");
        }
    }
}
