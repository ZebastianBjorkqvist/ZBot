using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Discord;
using System.Drawing;

namespace ZBot.Services
{
    class DiscordColorConverterService
    {
        public static Discord.Color ColorService(string colorString)
        {
            //This allows users to write colors that are part of KnownColor Enum https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=netcore-3.1
            System.Drawing.Color sysColor = System.Drawing.Color.FromName(colorString);

            if (sysColor.IsKnownColor)
            {
                return new Discord.Color(sysColor.R, sysColor.G, sysColor.B);
            }

            //This is for hex. Hex can start with # or be just six characters (eg. FFFFFF)
            if (colorString.StartsWith("#") || colorString.Length == 6)
            {
                System.Drawing.Color returnColor = System.Drawing.ColorTranslator.FromHtml(colorString);
                return new Discord.Color(returnColor.R, returnColor.G, returnColor.B);
            }

            //This is for rgb. It's possibly to input both with or without , as a separator
            colorString = colorString.Replace(",", "");

            if((int.TryParse(colorString.Substring(0, 3), out int r) &&
                int.TryParse(colorString.Substring(3, 3), out int g) &&
                int.TryParse(colorString.Substring(6, 3), out int b)))
            {
                return new Discord.Color(r, g, b);
            }

            //If nothing works return lightgrey
            return Discord.Color.LightGrey;
        }
    }
}
