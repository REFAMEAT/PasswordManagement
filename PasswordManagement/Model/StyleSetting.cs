using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.File;
using PasswordManagement.View;

namespace PasswordManagement.Model
{
    public class StyleSetting : ISetting
    {
        internal StyleSetting()
        {
            List<string> themeItems = new List<string>
            {
                "Dark",
                "Light"
            };

            ThemeItems = themeItems;

            List<Language> languageItems = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();

            LanguageItems = languageItems.ConvertAll(x => x.ToString());
            AllowedColors = ThemePatterns.SupportedColors;
        }

        internal List<string> ThemeItems { get; set; }

        internal List<string> LanguageItems { get; set; }

        internal List<string> AllowedColors { get; set; }

        internal string SelectedTheme { get; set; }

        internal string SelectedLanguage { get; set; }

        internal string SelectedColor { get; set; }

        public void Load()
        {
            ThemeData data = JsonHelper<ThemeData>.GetData(Globals.DefaultTheme);
            SelectedLanguage = data.Language.ToString();
            SelectedColor = data.PrimaryColor;
            SelectedTheme = data.Theme.ToString();
        }

        public void Save()
        {
            ThemeData themeData = new ThemeData
            {
                Language = Enum.Parse<Language>(SelectedLanguage),
                Theme = Enum.Parse<BaseTheme>(SelectedTheme),
                PrimaryColor = SelectedColor
            };

            JsonHelper<ThemeData>.WriteData(themeData);

            UiHelper.AdjustApplicationStyle(themeData);
        }
    }
}