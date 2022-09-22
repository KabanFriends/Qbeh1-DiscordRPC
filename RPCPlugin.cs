using BepInEx;
using BepInEx.Logging;
using Qbeh1_DiscordRPC.Common;
using Qbeh1_DiscordRPC.Patches;
using HarmonyLib;
using UnityEngine;
using System.Collections;
using System;

namespace Qbeh1_DiscordRPC
{
    [BepInPlugin(Constants.PLUGIN_GUID, Constants.PLUGIN_NAME, Constants.PLUGIN_VERSION)]
    public class RPCPlugin : BaseUnityPlugin
    {
        private static RPCPlugin instance;

        public static long startupTime;

        public static string levelName;

        private void Awake()
        {
            Logger.LogInfo($"{Constants.PLUGIN_NAME} (v{Constants.PLUGIN_VERSION}) by KabanFriends");

            instance = this;

            // Patch classes
            Harmony.CreateAndPatchAll(typeof(GameStatePatch));
            Harmony.CreateAndPatchAll(typeof(QbeditorPatch));
            Harmony.CreateAndPatchAll(typeof(LevelSettingsWindowPatch));
            Harmony.CreateAndPatchAll(typeof(OpenLevelWindowPatch));
            Harmony.CreateAndPatchAll(typeof(LoadLevelPatch));
            Harmony.CreateAndPatchAll(typeof(LevelLoaderPatch));

            // Initialize Discord
            startupTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).Ticks / 10000000L;

            DiscordHandler.Init();
            DiscordHandler.UpdatePresence(null, null);

            StartCoroutine(UpdateDiscord());
        }

        private void OnDestroy()
        {
            Logger.LogInfo($"Shutting down!");
            DiscordHandler.Shutdown();
        }

        IEnumerator UpdateDiscord()
        {
            for (; ; )
            {
                DiscordHandler.RunCallbacks();
                yield return new WaitForSeconds(.1f);
            }
        }

        public static void Log(string message)
            => Log(LogLevel.Info, message);

        public static void Log(LogLevel level, string message)
        {
            instance.Logger.Log(level, message);
        }
    }
}