using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Fluttershoe.Commands;
using Fluttershoe.Extensions;

namespace Fluttershoe
{
    public class FluttershoeSocketCommandContext : SocketCommandContext
    {
        public new FluttershoeSocketClient Client { get; }

        public FluttershoeSocketCommandContext(FluttershoeSocketClient client, SocketUserMessage msg) : base(client, msg)
        {
            Client = client;
        }

        public async Task CommandLog(string message)
        {
            await Client.FlutterConfig.LogConsole(new LogMessage(LogSeverity.Debug, CommandBase.CommandLogSourceName, message));
#if (!DEBUG)
            return;
#endif
            const int chunkSize = 1000;
            const int interval = 3000;

            var chunks = message.DivideString(chunkSize).ToList();
            foreach (var chunk in chunks)
            {
                var index = "";
                if (chunks.Count > 1)
                    index = $"[{chunks.IndexOf(chunk) + 1}/{chunks.Count}]{Environment.NewLine}";

                await Channel.SendMessageAsync($"{index}{chunk}".SintaxHighlightingMultiLine());
                await Task.Delay(interval);
            }
        }
    }
}