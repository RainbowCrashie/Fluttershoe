﻿using System;

namespace Fluttershoe.Extentions
{
    public static class DiscordExtentions
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