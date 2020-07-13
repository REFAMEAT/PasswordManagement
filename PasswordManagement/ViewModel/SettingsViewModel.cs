using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Theme;
using PasswordManagement.Backend.Xml;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class SettingsViewModel : NotifyPropertyChanged
    {
        private ICommand buttonCommandApplySettings;
        private string selectedColor;
        private string selectedLanguage;
        private string selectedTheme;

        public SettingsViewModel()
        {
            ThemeData data = JsonHelper.GetData();

            List<string> themeItems = new List<string>
            {
                "Dark", "Light"
            };

            ThemeItems = themeItems;

            List<Language> languageItems = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();

            LanguageItems = languageItems.ConvertAll(x => x.ToString());
            AllowedColors = ThemePatterns.SupportedColors;

            SelectedLanguage = data.Language.ToString();
            SelectedColor = data.PrimaryColor;
            SelectedTheme = data.Theme.ToString();
        }

        public List<string> ThemeItems { get; set; }

        public List<string> LanguageItems { get; set; }

        public List<string> AllowedColors { get; set; }

        public string SelectedTheme
        {
            get => selectedTheme;
            set => SetProperty(ref selectedTheme, value);
        }

        public string SelectedLanguage
        {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }

        public string SelectedColor
        {
            get => selectedColor;
            set => SetProperty(ref selectedColor, value);
        }

        public ICommand ButtonCommandApplySettings => buttonCommandApplySettings ??= new Command(DoApplySettings);

        private void DoApplySettings(object obj)
        {
            ThemeData themeData = new ThemeData
            {
                Language = Enum.Parse<Language>(SelectedLanguage),
                Theme = Enum.Parse<BaseTheme>(SelectedTheme),
                PrimaryColor = SelectedColor
            };

            JsonHelper.WriteData(themeData);

            App.AdjustApplicationStyle(themeData);
        }
    }
}