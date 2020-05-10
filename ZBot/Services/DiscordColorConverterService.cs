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
        public static Discord.Color ColorConverter(string colorString)
        {
            //This allows users to write colors that are part of KnownColor Enum https://docs.microsoft.com/en-us/dotnet/api/system.drawing.knowncolor?view=netcore-3.1
            System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorString);

            if (systemColor.IsKnownColor)
            {
                return new Discord.Color(systemColor.R, systemColor.G, systemColor.B);
            }

            //This is for hex. Hex can start with # or be just six characters (eg. FFFFFF)
            if (uint.TryParse(colorString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var color))
            {
                return new Discord.Color(color);
            }

            //This is for rgb. 
            var s = colorString.Split(",");

            if ((int.TryParse(s[0], out int r) &&
                int.TryParse(s[1], out int g) &&
                int.TryParse(s[2], out int b)))
            {
                return new Discord.Color(r, g, b);
            }

            //If nothing works return lightgrey
            return Discord.Color.LightGrey;
        }
    }
}
