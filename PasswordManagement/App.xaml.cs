using System;
using System.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Theme;
using PasswordManagement.Backend.Xml;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;
using PasswordManagement.View;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;

namespace PasswordManagement
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const bool DataBaseActive = false;
        public static string LogedIn;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ThemeData data = JsonHelper.GetData();

            if (data.Theme == BaseTheme.Inherit && data.PrimaryColor == null)
            {
                data = new ThemeData
                {
                    Language = Language.English,
                    PrimaryColor = "Blue",
                    Theme = BaseTheme.Light
                };
                JsonHelper.WriteData(data);
            }

            AdjustApplicationStyle(data);

            if (DataBaseActive && !new DataSet<USERDATA>().Entities.Any())
            {
                USERDATA firstUser = AddUser.CreateUser();
            }

            Login login = new Login();
            login.ShowDialog();
            LogedIn = login.passwordBox.Password;
            if (login.DialogResult == true)
            {
                MainWindow = new MainWindow();
                MainWindow.Closed += (o, e) => Shutdown(0);
                MainWindow.Show();
            }
            else
            {
                Shutdown();
            }
        }

        /// <summary>
        ///     Adjust the UI to the UI-Config
        /// </summary>
        /// <param name="data"></param>
        public static void AdjustApplicationStyle(ThemeData data)
        {
            Current.Resources.Clear();

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

            DColor color = DColor.Black;

            if (data.PrimaryColor != null) color = DColor.FromName(data.PrimaryColor);

            if (data.Theme == BaseTheme.Inherit) data.Theme = BaseTheme.Light;

            CustomColorTheme theme = new CustomColorTheme
            {
                BaseTheme = data.Theme,
                PrimaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B),
                SecondaryColor = MColor.FromArgb(color.A, color.R, color.G, color.B)
            };

            Current.Resources.MergedDictionaries.Add(languageDictionary);
            Current.Resources.MergedDictionaries.Add(styleDictionary);
            Current.Resources.MergedDictionaries.Add(theme);
        }
    }
}