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
    }
}