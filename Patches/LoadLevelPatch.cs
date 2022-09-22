using HarmonyLib;
using UnityEngine;

namespace Qbeh1_DiscordRPC.Patches
{
    public class LoadLevelPatch
    {
        [HarmonyPatch(typeof(LoadLevel), "Start")]
        [HarmonyPostfix]
        private static void StartPatch(LoadLevel __instance)
        {
            if (GameObject.Find("game_state"))
            {
                var gameState = GameObject.Find("game_state").GetComponent<GameState>();
                int world = int.Parse(gameState.levelToLoad.Substring(6, 1));
                int level = int.Parse(gameState.levelToLoad.Substring(8, 1));
                RPCPlugin.levelName = __instance.levelNames[(world - 1) * 6 + (level - 1)];
                DiscordHandler.UpdatePresence($"Level {world}-{level}:", "\"" + RPCPlugin.levelName + "\"");
            }
        }
    }
}
