using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.Services.Interfaces;
using REFame.PasswordManagement.WpfBase.Localization;
using MColor = System.Windows.Media.Color;

namespace REFame.PasswordManagement.App.View
{
    public class UiHelper
    {
        /// <summary>
        ///     Adjust the UI to the UI-Config
        /// </summary>
        /// <param name="data"></param>
        public static async Task AdjustApplicationStyle()
        {
            ThemeData data = await PWCore.CurrentCore.GetRegisteredType<ISettingService<ThemeData>>().Load();

            Application.Current.Resources.Clear();

            var languageDictionary = new ResourceDictionary
            {
                Source = Localizations.Current.GetRegisteredLanguageUri(data.Language)
            };

            var styleDictionary = new ResourceDictionary
            {
                Source = new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                    UriKind.Absolute)
            };

            var customStyleDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/REFame.PasswordManagement.Styles;component/TabControlStyles.xaml",
                    UriKind.Absolute)
            };

            Color color = Color.Black;

            if (data.PrimaryColor != null)
            {
                color = Color.FromName(data.PrimaryColor);
            }

            if (data.Theme == BaseTheme.Inherit)
            {
                data.Theme = BaseTheme.Light;
            }

            var theme = new CustomColorTheme
            {
                BaseTheme = data.Theme,
                PrimaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B),
                SecondaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B)
            };

            Application.Current.Resources.MergedDictionaries.Add(languageDictionary);
            Application.Current.Resources.MergedDictionaries.Add(styleDictionary);
            Application.Current.Resources.MergedDictionaries.Add(theme);
            Application.Current.Resources.MergedDictionaries.Add(customStyleDictionary);
        }
    }
}