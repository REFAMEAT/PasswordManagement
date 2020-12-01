using System;
using System.Collections.Generic;
using System.Linq;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.Settings.Contracts;
using REFame.PasswordManagement.WpfBase.Localization;

namespace REFame.PasswordManagement.Settings
{
    public class StyleSetting : ISetting
    {
        private readonly ISettingService<ThemeData> themeSettingService;

        public StyleSetting(ISettingService<ThemeData> themeSettingService)
        {
            this.themeSettingService = themeSettingService;

            var themeItems = new List<string>
            {
                "Dark",
                "Light"
            };

            ThemeItems = themeItems;

            // Get all registered Languages
            LanguageItems = Localizations
                .Current
                .GetAllLanguages()
                .ConvertAll(x => x.ToString());

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