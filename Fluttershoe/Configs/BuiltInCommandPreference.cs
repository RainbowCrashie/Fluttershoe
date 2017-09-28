using System;
using Discord;

namespace Fluttershoe.Configs
{
    public class BuiltInCommandPreference
    {
        public event EventHandler SaveEvent;
        public void Save() => SaveEvent?.Invoke(this, EventArgs.Empty);

        public string CreatorName { get; set; } = "Lauren Faust";
        public string RunningOn { get; set; } = "Unicorn Magic";
        public string Contacts { get; set; } = "unset";

        /// <summary>
        /// The default color for embeds. 
        /// </summary>
        public Color ThemeColor { get; set; } = Color.Blue;
    }
}