using System;
using System.Collections.Generic;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Implementations;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.WpfBase;
using REFame.PasswordManagement.WpfBase.Localization;
using REFame.PasswordManagement.WpfBase.Mediator;

namespace REFame.PasswordManagement.Settings.ViewModel.Tabs
{
    public class ThemeSettingsViewModel : BindableBase
    {
        private SettingMediator settingMediator;
        readonly ISettingService<ThemeData> themeSetting;

        public ThemeSettingsViewModel(ISettingService<ThemeData> themeSetting)
        {
            this.themeSetting = themeSetting ??= new ThemeSettingService();

            ThemeData data = themeSetting.Load().Result;

            SelectedColor = data.PrimaryColor;
            SelectedLanguage = data.Language.ToString();
            SelectedTheme = data.Theme.ToString();

            var themeItems = new List<string>
            {
                "Dark",
                "Light"
            };

            ThemeItems = themeItems;

            // Get all registered Languages
            LanguageItems = AppLocalization
                .Current
                .GetPossibleLanguages()
                .ConvertAll(x => x.ToString());

            AllowedColors = ThemePatterns.SupportedColors;
        }

        public SettingMediator SettingMediator
        {
            get => settingMediator;
            set
            {
                settingMediator = value;
                settingMediator.SaveRequested += SettingMediatorOnSaveRequested;
            }
        }

        public List<string> ThemeItems { get; }
        public List<string> LanguageItems { get; }
        public List<string> AllowedColors { get; }

        public string SelectedTheme { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedColor { get; set; }

        public async void SettingMediatorOnSaveRequested(object sender, EventArgs e)
        {
            ThemeData newTheme = new ThemeData
            {
                Language = SelectedLanguage,
                Theme = Enum.Parse<BaseTheme>(SelectedTheme),
                SecondaryColor = SelectedColor,
                PrimaryColor = SelectedColor
            };

            await themeSetting.Save(newTheme);
            ThemeMediator.RequestChangeTheme(newTheme);
        }
    }
}