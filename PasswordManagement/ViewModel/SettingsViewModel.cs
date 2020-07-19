using System;
using System.Collections.Generic;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Xml;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class SettingsViewModel : NotifyPropertyChanged
    {
        private ICommand buttonCommandApplySettings;
        private string selectedTheme;
        private string selectedColor;
        private string selectedLanguage;

        public SettingsViewModel()
        {
            List<string> themeItems = new List<string>()
            {
                "Dark", "Light"
            };
            
            ThemeItems = themeItems;

            List<string> languageItems = new List<string>();
            foreach (Language item in Enum.GetValues(typeof(Language)))
            {
                languageItems.Add(item.ToString());
            }

            LanguageItems = languageItems;

            AllowedColors = new List<string>()
            {
                "Blue", "Yellow", "Orange", "Black", "Turquoise", "LimeGreen", "BlueViolet", "Lime"
            };

            XmlData data = new XmlHelper().GetData();

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
            XmlData xmlData = new XmlData()
            {
                Language = Enum.Parse<Language>(SelectedLanguage),
                Theme = Enum.Parse<BaseTheme>(SelectedTheme),
                PrimaryColor = SelectedColor,
            };

            new XmlHelper().Write(xmlData);

            App.AdjustApplicationStyle(xmlData);
        }
    }
}