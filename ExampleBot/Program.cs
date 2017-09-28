using System;
using System.Dynamic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Fluttershoe;
using Fluttershoe.Configs;

namespace ExampleBot
{
    public class Program
    {
        private static FluttershoeSocketClient Client { get; set; }

        private static async Task Main()
        {
            var token = TokenIsStoredLocallyToAvoidGitHubLeakage();

            Client = new FluttershoeSocketClient(
                new FluttershoeConfig
                {
                    Token = token,
                    CommandPrefix = '!',
                    CommandPrefixDebug = '$',
                    Playing = "Titanfall 2"
                },
                new BuiltInCommandPreference
                {
                    CreatorName = "RainbowCrashie",
                    RunningOn = "a PC",
                    Contacts = "[`GitHub`](https://github.com/RainbowCrashie)",
                    ThemeColor = new Color(0,0,255)
                },
                new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug
                },
                new CommandServiceConfig
                {
                    CaseSensitiveCommands = false
                });

            await Client.Start();

            await Task.Delay(-1);
        }

        private static string TokenIsStoredLocallyToAvoidGitHubLeakage()
        {
            //for the sake of security for contributors.
            //Unnecessary for private bots.
            string token;
            var tokenPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "token");
            if (!File.Exists(tokenPath))
            {
                Console.Write("Welcome to example bot! Paste your bot token: ");
                token = Console.ReadLine();
                Console.Clear();
                File.WriteAllText(tokenPath, token);
            }
            token = File.ReadAllText(tokenPath);

            return token;
        }
    }

}
