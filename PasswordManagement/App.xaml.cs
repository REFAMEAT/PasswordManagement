using System;
using System.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Xml;
using PasswordManagement.Database.DbSet;
using MColor = System.Windows.Media.Color;
using DColor = System.Drawing.Color;
using PasswordManagement.Database.Model;
using PasswordManagement.View;
using System.Reflection;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Theme;

namespace PasswordManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string LogedIn;
        public const bool DataBaseActive = false;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ThemeData data = JsonHelper.GetData();

            if (data.Theme == BaseTheme.Inherit && data.PrimaryColor == null)
            {
                data = new ThemeData()
                {
                    Language = Language.English,
                    PrimaryColor = "Blue",
                    Theme = BaseTheme.Light,
                };
               JsonHelper.WriteData(data);
            }

            AdjustApplicationStyle(data);

            if (!new DataSet<USERDATA>().Entities.Any() && DataBaseActive)
            {
                USERDATA firstUser = AddUser.CreateUser();
            }

            MainWindow = new MainWindow();
            Login login = new Login();
            login.ShowDialog();
            LogedIn = login.passwordBox.Password;
            if (login.DialogResult == true)
            {
                MainWindow.Show(); 
            }
            else
            {
                MainWindow.Close();
            }

        }

        /// <summary>
        /// Adjust the UI to the UI-Config
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

            ResourceDictionary styleDictionary = new ResourceDictionary()
            {
                Source = new Uri(
                    "pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml",
                    UriKind.Absolute)
            };

            DColor color = DColor.Black;

            if (data.PrimaryColor != null)
            {
                color = DColor.FromName(data.PrimaryColor);
            }

            if (data.Theme == BaseTheme.Inherit)
            {
                data.Theme = BaseTheme.Light;
            }

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
