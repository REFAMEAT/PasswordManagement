using System.Windows;
using MaterialDesignThemes.Wpf;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Backend.Login;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Enums;
using REFame.PasswordManagement.Model.Setting;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App
{
    internal static class Setup
    {
        internal static AppCore StartUpUi(this AppCore app)
        {
            ThemeMediator.ChangeThemeRequested +=
                (sender, args) => UiHelper.AdjustApplicationStyle(args.NewTheme);

            var theme = JsonHelper<ThemeData>.GetData(Globals.DefaultTheme);

            if (theme.Theme != BaseTheme.Inherit || theme.PrimaryColor != null)
            {
                ThemeMediator.RequestChangeTheme(theme);
                return app;
            }

            theme = new ThemeData
            {
                Language = Language.English,
                PrimaryColor = "Blue",
                Theme = BaseTheme.Light
            };
            JsonHelper<ThemeData>.WriteData(theme);
            ThemeMediator.RequestChangeTheme(theme);
            return app;
        }

        internal static AppCore Login(this AppCore app)
        {
            bool useDatabase = JsonHelper<DatabaseData>.GetData(new DatabaseData { UseDatabase = false }).UseDatabase;

            Login login = useDatabase ? new Login(new DatabaseLogin()) : new Login(new LocalLogin());

            login.ShowDialog();

            if (login.DialogResult != true)
            {
                Application.Current.Shutdown(1);
            }
            else
            {
                App.loginPw = login.passwordBox.Password;
            }

            return app;
        }

        internal static void StartMain(this AppCore app)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Closed += (o, args) => Application.Current.Shutdown(0);
            Application.Current.MainWindow?.Show();
        }
    }
}