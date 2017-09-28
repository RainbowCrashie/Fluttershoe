using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Fluttershoe.Configs;

namespace Fluttershoe.Commands
{
    public class CommandBase : ModuleBase<FluttershoeSocketCommandContext>
    {
        #region Fields and Properties

        protected static Color EmbedColor = new Color(31, 151, 242);

        protected IDisposable TypingIndicator => Context.Channel.EnterTypingState();
        private Stopwatch Stopwatch { get; } = new Stopwatch();

        public static string CommandLogSourceName { get; } = "Command Execution";

        #endregion

        protected void StartStopwatch()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }

        protected async Task<long> RecordStopwatch(string caption = "", bool continueIt = true)
        {
            Stopwatch.Stop();

            var duration = Stopwatch.ElapsedMilliseconds;
            var message = $"{caption}: {Stopwatch.ElapsedMilliseconds}ms";

            await Context.CommandLog(message);

            if (continueIt)
                StartStopwatch();

            return duration;
        }

        #region Replying Methods
        protected async Task<RestUserMessage> ReplyEmbed(Embed embed, string text = "")
        {
            return await Context.Channel.SendMessageAsync(text, false, embed);
        }

        protected async Task ReplyVolatile(string text, int durationSeconds = 10)
        {
            var message = await Context.Channel.SendMessageAsync(text);
            await Task.Delay(durationSeconds * 1000);
            await message.DeleteAsync();
        }

        protected EmbedBuilder EmbedBuilderFactory()
        {
            var embed = new EmbedBuilder();
            embed.Color = Context.Client.BuiltInCommandPreference.ThemeColor;

            return embed;
        }
        #endregion

    }
}