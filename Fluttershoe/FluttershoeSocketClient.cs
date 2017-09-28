using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Fluttershoe.Configs;

namespace Fluttershoe
{
    public class FluttershoeSocketClient : DiscordSocketClient
    {
        public FluttershoeConfig FlutterConfig { get; }
        public BuiltInCommandPreference BuiltInCommandPreference { get; }
        private CommandServiceConfig CommandConfig { get; }

        public CommandHandler CommandHandler { get; private set; }

        public FluttershoeSocketClient(
            FluttershoeConfig flutterConfig,
            BuiltInCommandPreference builtInCommandPreference = null,
            DiscordSocketConfig discordConfig = null,
            CommandServiceConfig commandConfig = null)
            : base(discordConfig ?? new DiscordSocketConfig {LogLevel = LogSeverity.Debug})
        {
            FlutterConfig = flutterConfig;
            BuiltInCommandPreference = builtInCommandPreference ?? new BuiltInCommandPreference();
            CommandConfig = commandConfig ?? new CommandServiceConfig();
        }

        public FluttershoeSocketClient(FluttershoeConfig flutterConfig)
            : this(flutterConfig, null)
        {}

        public async Task Start()
        {
            Log += FlutterConfig.LogConsole;

            await LoginAsync(TokenType.Bot, FlutterConfig.Token);
            await StartAsync();

            CommandHandler = new CommandHandler(this, CommandConfig);
            await CommandHandler.InitializeAsync();

            await Task.Delay(5000); //Discord doesn't recognise SetGame when it's requested too soon.
            await SetGameAsync(FlutterConfig.Playing);
        }
        
        public async Task Stop()
        {
            Log -= FlutterConfig.LogConsole;
            await StopAsync();
        }

        public async Task Restart()
        {
            await Stop();
            await Start();
        }
    }


}
