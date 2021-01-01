using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App
{
    public class WpfCore
    {
        public static WpfCore Current { get; set; } = new WpfCore();

        private WpfCore() { }

        internal bool Login(ICore appCore)
        {
            ILogin loginMethod = appCore.GetRegisteredType<ILogin>();
            View.Login login = new View.Login(loginMethod);

            Application.Current.MainWindow = login;

            login.ShowDialog();

            if (login.DialogResult != true)
            {
                return false;
            }
            else
            {
                App.loginPw = login.passwordBox.Password;
                return true;
            }
        }

        internal void RegisterMainWindow<T>() where T : Window
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Closed += (o, args) => Application.Current.Shutdown(0);
            Application.Current.MainWindow?.Show();
        }
    }
}