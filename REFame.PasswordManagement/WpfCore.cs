using System.Windows;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.App
{
    public class WpfCore
    {
        public static WpfCore Current { get; set; } = new WpfCore();

        private WpfCore()
        {

        }

        internal void Login(ICore appCore)
        {
            ILogin loginMethod = appCore.GetRegisteredType<ILogin>();
            View.Login login = new View.Login(loginMethod);
            login.ShowDialog();

            if (login.DialogResult != true)
            {
                Application.Current.Shutdown(1);
            }
            else
            {
                App.loginPw = login.passwordBox.Password;
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