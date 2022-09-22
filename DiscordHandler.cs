using System;
using BepInEx.Logging;
using Qbeh1_DiscordRPC.Common;

namespace Qbeh1_DiscordRPC
{
    public class DiscordHandler
    {
        private static Discord.Discord discord;

        public static void Init()
        {
            try
            {
                discord = new Discord.Discord(Int64.Parse(Constants.CLIENT_ID), (UInt64)Discord.CreateFlags.Default);
            } catch (Discord.ResultException e)
            {
                RPCPlugin.Log(LogLevel.Error, "Couldn't start Discord client: " + e.StackTrace);
                return;
            }

            discord.SetLogHook(Discord.LogLevel.Info, (level, message) =>
            {
                RPCPlugin.Log(GetPluginLogLevel(level), message);
            });
        }

        public static void UpdatePresence(string details, string state)
        {
            if (discord == null)
            {
                return;
            }

            var activity = new Discord.Activity
            {
                State = state,
                Details = details,
                Timestamps =
                {
                    Start = RPCPlugin.startupTime,
                    End = 0L
                },
                Assets =
                {
                    LargeImage = "qbeh-1"
                }
            };

            UpdateActivity(activity);
        }

        public static void RunCallbacks()
        {
            if (discord == null)
            {
                return;
            }

            discord.RunCallbacks();
        }

        public static void Shutdown()
        {
            if (discord == null)
            {
                return;
            }

            discord.Dispose();
            discord = null;
        }

        private static void UpdateActivity(Discord.Activity activity)
        {
            if (discord == null)
            {
                return;
            }

            discord.GetActivityManager().UpdateActivity(activity, result => {});
        }

        private static LogLevel GetPluginLogLevel(Discord.LogLevel level)
        {
            switch (level)
            {
                case Discord.LogLevel.Debug:
                    return LogLevel.Debug;
                case Discord.LogLevel.Info:
                    return LogLevel.Info;
                case Discord.LogLevel.Warn:
                    return LogLevel.Warning;
                case Discord.LogLevel.Error:
                    return LogLevel.Error;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
