using System;

namespace Fluttershoe.Extensions
{
    public static class DiscordExtensions
    {
       
        public static string SintaxHighlightingMultiLine(this string str, string language = "")
        {
            const string codeFormattingMultiLine = "```";
            return $"{codeFormattingMultiLine}{language}{Environment.NewLine}{str}{Environment.NewLine}{codeFormattingMultiLine}";
        }
        
        public static string SintaxHighlighted(this string str, string language = "")
        {
            const string codeFormatting = "`";
            return $"{codeFormatting}{language}{Environment.NewLine}{str}{Environment.NewLine}{codeFormatting}";
        }
    }
}