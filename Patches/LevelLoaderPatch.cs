using HarmonyLib;
using UnityEngine;

namespace Qbeh1_DiscordRPC.Patches
{
    public class LevelLoaderPatch
    {
        [HarmonyPatch(typeof(LevelLoader), "GetLevelName")]
        [HarmonyPostfix]
        private static void GetLevelNamePatch(string __result)
        {
            if (Application.loadedLevelName.Equals("level_community"))
            {
                DiscordHandler.UpdatePresence("Community Level:", "\"" + __result + "\"");
            }
        }
    }
}
