using System;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Fluttershoe.Commands;

namespace Fluttershoe.Configs
{
    public class FluttershoeConfig
    {
        public event EventHandler SaveConfig;

        public string Token { get; set; }

        public char CommandPrefix { get; set; }

        /// <summary>
        /// The "Debug" build binary will exclusively interprets comments with this prefix as commands. Leave it blank if you don't need this.
        /// </summary>
        public char CommandPrefixDebug { get; set; }

        public string Playing { get; set; } = "";
        
        /// <summary>
        /// Log output function. Modify if you prefer to use your own function.
        /// </summary>
        public Func<LogMessage, Task> LogConsole { get; set; } = message =>
        {
            const char bracketOpening = '[';
            const string bracketClosing = "] ";
            const string dateTimeFormat = "T";

            if (message.Severity == LogSeverity.Debug)
                Console.ForegroundColor = ConsoleColor.DarkGray;

            if (message.Source == CommandBase.CommandLogSourceName)
                Console.ForegroundColor = ConsoleColor.Gray;

            //time stamp
            Console.Write(new StringBuilder()
                .Append(bracketOpening)
                .Append(DateTime.Now.ToString(dateTimeFormat))
                .Append(bracketClosing));

            //Red font for serious messages
            if (message.Severity == LogSeverity.Critical ||
                message.Severity == LogSeverity.Error ||
                message.Severity == LogSeverity.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(new StringBuilder()
                    .Append(bracketOpening)
                    .Append(message.Severity)
                    .Append(bracketClosing));
            }

            //body
            Console.Write(new StringBuilder()
                .Append(bracketOpening)
                .Append(message.Source)
                .Append(bracketClosing)

                .Append(message.Message)
                .Append(Environment.NewLine));

            //reset color
            Console.ForegroundColor = ConsoleColor.White;
            return Task.CompletedTask;
        };
    }
}