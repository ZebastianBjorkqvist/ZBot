using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace ZBot.Services
{
    class ColorConverterService
    {
        public static int[] HexToRGB(string colorString)
        {
            //replace # occurences
            if (colorString.IndexOf('#') != -1)
            { 
                colorString = colorString.Replace("#", "");
            }

            foreach (char c in colorString)
            {
                if (c == ',')
                {
                    colorString = colorString.Replace(",", "");
                }
            }

            if (colorString.Length > 6)
            {
                int[] resultRgb = { int.Parse(colorString.Substring(0, 2)), int.Parse(colorString.Substring(2, 2)), int.Parse(colorString.Substring(4, 2)) };

                return resultRgb;
            }

            int r = int.Parse(colorString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            int g = int.Parse(colorString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            int b = int.Parse(colorString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            int[] result = { r, g, b };

            return result;
        }
    }
}
