using System;
using System.Drawing;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Model.Enums;
using PasswordManagement.Model.Setting;
using MColor = System.Windows.Media.Color;

namespace PasswordManagement.View
{
    public class UiHelper
    {
        /// <summary>
        ///     Adjust the UI to the UI-Config
        /// </summary>
        /// <param name="data"></param>
        internal static void AdjustApplicationStyle(ThemeData data)
        {
            Application.Current.Resources.Clear();

            ResourceDictionary languageDictionary = new ResourceDictionary();
            languageDictionary.Source = data.Language switch
            {
                Language.English => new Uri("..\\Resources\\StringResources.EN.xaml", UriKind.Relative),
                Language.German => new Uri("..\\Resources\\StringResources.DE.xaml", UriKind.Relative),
                _ => languageDictionary.Source
            };

            ResourceDictionary styleDictionary = new ResourceDictionary
            {
                Source = new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                    UriKind.Absolute)
            };

            ResourceDictionary customStyleDictionary = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/PasswordManagement.Styles;component/TabControlStyles.xaml", UriKind.Absolute)
            };

            Color color = Color.Black;

            if (data.PrimaryColor != null) color = Color.FromName(data.PrimaryColor);

            if (data.Theme == BaseTheme.Inherit) data.Theme = BaseTheme.Light;

            CustomColorTheme theme = new CustomColorTheme
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