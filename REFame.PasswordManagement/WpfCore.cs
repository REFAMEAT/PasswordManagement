using System;
using System.Windows;
using REFame.PasswordManagement.AppCore;
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
            var login = appCore.GetRegisteredType<View.Login>();
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

        }

        internal void ShowMainWindow()
        {
            Application.Current.MainWindow.Show();
            (Application.Current.MainWindow as IInitializable)?.Initialize();
        }
    }
}