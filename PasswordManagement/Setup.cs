using System.Windows;
using PasswordManagement.Model;
using PasswordManagement.Model.Setting;
using PasswordManagement.View;

namespace PasswordManagement
{
    internal static class Setup
    {
        internal static AppCore StartUpUi(this AppCore app, ThemeData theme)
        {
            UiHelper.AdjustApplicationStyle(theme);
            return app;
        }

        internal static AppCore Login(this AppCore app, out bool loginSuccess)
        {
            Login login = new Login();
            login.ShowDialog();

            if (login.DialogResult == true)
            {
                App.loginPw = login.passwordBox.Password;
                loginSuccess = true;
            }
            else
            {
                loginSuccess = false;
            }

            return app;
        }

        internal static void StartMain(this AppCore app, bool loginSuccess)
        {
            if (!loginSuccess)
            {
                return;
            }

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Closed += (o, args) => Application.Current.Shutdown(0);
            Application.Current.MainWindow?.Show();
        }
    }
}