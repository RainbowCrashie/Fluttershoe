using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Fluttershoe.Extensions;
using Microsoft.Extensions.DependencyModel;

namespace Fluttershoe
{
    public class CommandHandler 
    {
        #region Fields and Properties

        private FluttershoeSocketClient Client { get; }
        public CommandService CommandService { get; }

        #endregion

        #region Ctor
        public CommandHandler(FluttershoeSocketClient client, CommandServiceConfig config)
        {
            Client = client;

            CommandService = new CommandService(config);
        }
        #endregion

        #region Methods
        public async Task InitializeAsync()
        {
            foreach (var assembly in DependencyContext.Default.RuntimeLibraries)
            {
                try
                {
                    await CommandService.AddModulesAsync(Assembly.Load(new AssemblyName(assembly.Name)));
                }
                catch (Exception) { } //Ignore errors, never going to need.
            }

            Client.MessageReceived += HandleCommandAsync;
        }

        public async Task HandleCommandAsync(SocketMessage s)
        {
            var message = s as SocketUserMessage;
            if (message == null)
                return;

            var context = new FluttershoeSocketCommandContext(Client, message);

            //Prefix handling
            var prefix = default(char);
            #if DEBUG
                if (Client.FlutterConfig.CommandPrefixDebug != default(char))
                    prefix = Client.FlutterConfig.CommandPrefixDebug;
                else
                    prefix = Client.FlutterConfig.CommandPrefix;
            #else
                prefix = Client.FlutterConfig.CommandPrefix;
            #endif

            var argPosition = 0;
            if (message.HasCharPrefix(prefix, ref argPosition))
            {
                await context.CommandLog($"{context.User} executed command: {message} (at {context.Guild?.Name} {context.Channel.Name})");
                var result = await CommandService.ExecuteAsync(context, argPosition);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    var r = (ExecuteResult)result;
                    await context.Channel.SendMessageAsync(r.Exception.Message.SintaxHighlightingMultiLine());
                    await context.CommandLog(r.ToString());
                }
            }
        }
        #endregion
    }
}