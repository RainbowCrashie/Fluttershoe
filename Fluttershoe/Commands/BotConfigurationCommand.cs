using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Fluttershoe.Configs;
using Fluttershoe.Extentions;

namespace Fluttershoe.Commands
{
    //TODO:Disable if public bot
    [Group("botconf"), RequireUserPermission(GuildPermission.Administrator)]
    public class BotConfigurationCommand : CommandBase
    {
        private FluttershoeConfig Config => Context.Client.FlutterConfig;
        private BuiltInCommandPreference BuiltInCommandPreference => Context.Client.BuiltInCommandPreference;

        #region InfoCommand
        [Command("creator")]
        public async Task CreatorName(string name)
        {
            BuiltInCommandPreference.CreatorName = name;
            BuiltInCommandPreference.Save();
            await ReplyAsync("`CreatorName Modified Successfully`");
        }
        [Command("creator")]
        public async Task CreatorName()
        {
            await ReplyAsync($"BuiltInCommandPreference.CreatorName is set to: {BuiltInCommandPreference.CreatorName}".SintaxHighlightingMultiLine());
        }

        [Command("contacts")]
        public async Task Contacts(string text)
        {
            BuiltInCommandPreference.Contacts = text;
            BuiltInCommandPreference.Save();
            await ReplyAsync("`Contacts Modified Successfully`");
        }
        [Command("creator")]
        public async Task Contacts()
        {
            await ReplyAsync($"BuiltInCommandPreference.CreatorName is set to: {BuiltInCommandPreference.Contacts}".SintaxHighlightingMultiLine());
        }

        [Command("runningon")]
        public async Task RunningOn(string text)
        {
            BuiltInCommandPreference.RunningOn = text;
            BuiltInCommandPreference.Save();
            await ReplyAsync("RunningOn Modified Successfully`");
        }
        [Command("runningon")]
        public async Task RunningOn()
        {
            await ReplyAsync($"BuiltInCommandPreference.RunningOn is set to: {BuiltInCommandPreference.RunningOn}".SintaxHighlightingMultiLine());
        }
        #endregion
    }
}