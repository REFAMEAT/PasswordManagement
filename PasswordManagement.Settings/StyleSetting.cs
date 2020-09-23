using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Model.Enums;
using PasswordManagement.Model.Interfaces;
using PasswordManagement.Model.Setting;
using PasswordManagement.Services.Interfaces;

namespace PasswordManagement.Settings
{
    public class StyleSetting : ISetting
    {
        private readonly ISettingService<ThemeData> themeSettingService;

        public StyleSetting(ISettingService<ThemeData> themeSettingService)
        {
            this.themeSettingService = themeSettingService;

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

        public List<string> ThemeItems { get; set; }

        public List<string> LanguageItems { get; set; }

        public List<string> AllowedColors { get; set; }

        public string SelectedTheme { get; set; }

        public string SelectedLanguage { get; set; }

        public string SelectedColor { get; set; }

        public void Load()
        {
            ThemeData data = themeSettingService.Load();
            SelectedLanguage = data.Language.ToString();
            SelectedColor = data.PrimaryColor;
            SelectedTheme = data.Theme.ToString();
        }

        public void Save()
        {
            themeSettingService.Save(new ThemeData
            {
                Language = Enum.Parse<Language>(SelectedLanguage),
                Theme = Enum.Parse<BaseTheme>(SelectedTheme),
                PrimaryColor = SelectedColor
            });

            themeSettingService.OnSaved(EventArgs.Empty);
        }
    }
}