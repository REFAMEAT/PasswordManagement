using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using PasswordManagement.Backend.Json;
using PasswordManagement.Backend.Settings;
using PasswordManagement.Backend.Xml;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;
using PasswordManagement.Logging;
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
        public static string fileRoot =
            @"C:\Users\{user}\AppData\Roaming\PWManagement".Replace("{user}", Environment.UserName);

        public static string LogedIn;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Directory.Exists(fileRoot))
            {
                Directory.CreateDirectory(fileRoot);
            }

            Current.DispatcherUnhandledException += (o, args) => Logger.Current.Error(args.Exception);
            ThemeData data = JsonHelper.GetData();

            if (data.Theme == BaseTheme.Inherit && data.PrimaryColor == null)
            {
                data = new ThemeData
                {
                    Language = Language.English,
                    PrimaryColor = "Blue",
                    Theme = BaseTheme.Light
                };
                await JsonHelper.WriteDataAsync(data);
            }

            AdjustApplicationStyle(data);

            if (DataBaseActive && !new DataSet<USERDATA>().Entities.Any())
            {
                USERDATA firstUser = AddUser.CreateUser();
            }
            
            Login login = new Login();
            login.ShowDialog();
            
            if (login.DialogResult == true)
            {
                LogedIn = login.passwordBox.Password;

                MainWindow = new MainWindow();
                MainWindow.Closed += (o, args) => Shutdown(0);
                MainWindow.Show();
            }
            else
            {
                Shutdown(1);
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

            ResourceDictionary customStlyeDictionary = new ResourceDictionary()
            {
                Source = new Uri("../Styles/TabControlStyles.xaml", UriKind.Relative)
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
            Current.Resources.MergedDictionaries.Add(customStlyeDictionary);

            Logger.Current.Debug($"Changed Application Style to: {theme.BaseTheme}");
        }
    }
}