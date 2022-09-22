using HarmonyLib;

namespace Qbeh1_DiscordRPC.Patches
{
    public class QbeditorPatch
    {
        [HarmonyPatch(typeof(Qbeditor), "SetupNewLevel")]
        [HarmonyPrefix]
        private static void NewLevelPatch(string name)
        {
            DiscordHandler.UpdatePresence("Editing a level:", "\"" + name + "\"");
        }
    }
}
