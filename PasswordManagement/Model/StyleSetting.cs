using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.View;

namespace PasswordManagement.Model
{
    public class StyleSetting : ISetting
    {
        public StyleSetting()
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

        public List<string> ThemeItems { get; set;  }

        public List<string> LanguageItems { get; set; }

        public List<string> AllowedColors { get; set; }

        public string SelectedTheme { get; set; }

        public string SelectedLanguage { get; set; }

        public string SelectedColor { get; set; }

        public void Load()
        {
            ThemeData data = JsonHelper.GetData();
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

            JsonHelper.WriteData(themeData);

            UiHelper.AdjustApplicationStyle(themeData);
        }
    }
}