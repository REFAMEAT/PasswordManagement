using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Xml;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace PasswordManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            XmlData data = new XmlHelper().GetData();
            AdjustApplicationStyle(data);
        }
        
        /// <summary>
        /// Adjust the UI to the UI-Config
        /// </summary>
        /// <param name="data"></param>
        public static void AdjustApplicationStyle(XmlData data)
        {
            Current.Resources.Clear();

            ResourceDictionary languageDictionary = new ResourceDictionary();
            languageDictionary.Source = data.Language switch
            {
                Language.English => new Uri("..\\Resources\\StringResources.EN.xaml", UriKind.Relative),
                Language.German => new Uri("..\\Resources\\StringResources.DE.xaml", UriKind.Relative),
                _ => languageDictionary.Source
            };

            ResourceDictionary styleDictionary = new ResourceDictionary()
            {
                Source = new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                    UriKind.Absolute)
            };

            DColor color = DColor.FromName(data.PrimaryColor);

            CustomColorTheme theme = new CustomColorTheme()
            {
                BaseTheme = data.Theme,
                PrimaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B),
                SecondaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B),
            };

            Current.Resources.MergedDictionaries.Add(languageDictionary);
            Current.Resources.MergedDictionaries.Add(styleDictionary);
            Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}
