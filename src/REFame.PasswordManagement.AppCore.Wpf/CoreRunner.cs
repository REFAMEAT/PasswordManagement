using System;
using System.Drawing;
using System.Threading;
using System.Windows;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.Localization;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.AppCore.Wpf
{
    public static class CoreRunner
    {
        public static void RunWpf(this ICore core)
        {
            var culture = core.CoreInformation.AppCultureInfo;
            var loginHandler = core.CoreInformation.LoginHandler;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            Loc.Culture = culture;

            InitializeUI(core.CoreInformation.ThemeData);

            bool successful = HandleLogin(loginHandler);

            // Shutdown app if first user creating or login is not successful
            if (!successful)
            {
                Application.Current.Shutdown();
            }
            else
            {
                Application.Current.MainWindow = core.Resolve(core.CoreInformation.MainWindow) as Window;

                if (Application.Current.MainWindow is { } window)
                {
                    window.DataContext = core.Resolve(core.CoreInformation.MainViewModel);
                    window.Show();
                }
            }
        }

        private static bool HandleLogin(ILoginHandler loginHandler)
        {
            if (!loginHandler.CreateFirstUserIfNeeded())
            {
                return false;
            }

            if (!loginHandler.Login())
            {
                return false;
            }

            return true;
        }

        private static void InitializeUI(ThemeData data)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

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
                PrimaryColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B),
                SecondaryColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B)
            };

            Application.Current.Resources.MergedDictionaries.Add(styleDictionary);
            Application.Current.Resources.MergedDictionaries.Add(theme);
            Application.Current.Resources.MergedDictionaries.Add(customStyleDictionary);
        }
    }
}