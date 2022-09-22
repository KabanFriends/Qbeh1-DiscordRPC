using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Qbeh1_DiscordRPC.Patches
{
    public static class GameStatePatch
    {
        [HarmonyPatch(typeof(GameState), "OnLevelWasLoaded")]
        [HarmonyPostfix]
        private static void LevelLoadPatch()
        {
            switch (Application.loadedLevelName)
            {
                case "level_selection":
                    DiscordHandler.UpdatePresence("Level Selection", null);
                    break;
                case "level_selection_community":
                    DiscordHandler.UpdatePresence("Community Menu", null);
                    break;
                case "menu_main":
                    DiscordHandler.UpdatePresence("Main Menu", null);
                    break;
                case "credits":
                    DiscordHandler.UpdatePresence("Credits", null);
                    break;
                case "ending_final":
                    DiscordHandler.UpdatePresence("Ending", null);
                    break;
                default:
                    if (Application.loadedLevelName.StartsWith("level_"))
                    {
                        string text = Application.loadedLevelName.Replace("level_", "");
                        if (Regex.IsMatch(text, "[0-9]-[0-9]"))
                        {
                            DiscordHandler.UpdatePresence("Level " + text + ":", "\"" + RPCPlugin.levelName + "\"");
                        }
                    }
                    break;
            }
        }
    }
}
