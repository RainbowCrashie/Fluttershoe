using System;
using Discord.WebSocket;

namespace Fluttershoe.Commands
{
    public class GuildCommandBase : CommandBase
    {
        /// <summary>
        /// Validations are done by default.
        /// </summary>
        protected SocketVoiceChannel CallerCurrentVoiceChannel()
        {
            var user = Context.User as SocketGuildUser;
            OperatedOnGuildValidation(user);

            var channel = user.VoiceChannel;
            VoiceChannelConnectedValidation(channel);

            return channel;
        }

        protected static void OperatedOnGuildValidation(SocketGuildUser user)
        {
            if (user == null)
                throw new InvalidOperationException("Command not called on a server");
        }

        protected static void VoiceChannelConnectedValidation(SocketVoiceChannel channel)
        {
            if (channel == null)
                throw new InvalidOperationException("You are not connected to a voice channel");
        }
    }
}