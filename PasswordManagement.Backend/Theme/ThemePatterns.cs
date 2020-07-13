using System;
using System.Collections.Generic;

namespace PasswordManagement.Backend.Theme
{
    public static class ThemePatterns
    {
        public static List<string> SupportedColors = new List<string>
        {
            "Blue",
            "LightBlue",
            "Yellow",
            "Orange",
            "Black",
            "Turquoise",
            "LimeGreen",
            "BlueViolet",
            "Lime"
        };

        public static List<Language> supportedLanguages = new List<Language>
        {
            Language.English,
            Language.German,
        };
    }
}