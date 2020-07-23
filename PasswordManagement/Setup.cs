using System.Windows;
using PasswordManagement.Backend;
using PasswordManagement.Backend.Xml;
using PasswordManagement.View;

namespace PasswordManagement
{
    public static class Setup
    {
        public static AppCore StartUpUi(this AppCore app, ThemeData theme)
        {
            UiHelper.AdjustApplicationStyle(theme);
            return app;
        }

        public static AppCore Login(this AppCore app, out bool loginSuccess)
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

        public static void StartMain(this AppCore app, bool loginSuccess)
        {
            if (!loginSuccess)
            {
                return;
            }

            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Closed += (o, args) => Application.Current.Shutdown(0);
            Application.Current.MainWindow.Show();
        }
    }
}