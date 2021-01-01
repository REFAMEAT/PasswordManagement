using System.Windows;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Contracts;

namespace REFame.PasswordManagement.App
{
    public class WpfCore
    {
        public static WpfCore Current { get; set; } = new WpfCore();

        private WpfCore() { }

        internal bool Login(ICore appCore)
        {
            var login = appCore.GetRegisteredType<View.Login>();
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
            Application.Current.MainWindow = PWCore.CurrentCore.GetRegisteredType<T>();
            Application.Current.MainWindow.Closed += (o, args) => Application.Current.Shutdown(0);
            Application.Current.MainWindow?.Show();
        }
    }
}