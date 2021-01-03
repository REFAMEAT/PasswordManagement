using System.Windows.Input;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private readonly ILogin iLogin;
        private ICommand buttonCommandLogin;
        private string userName;

        public LoginViewModel(ILogin logonMethod)
        {
            iLogin = logonMethod;
            iLogin.Initialize();
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ICommand ButtonCommandLogin => buttonCommandLogin ??= new Command(DoLogin);

        private void DoLogin(object obj)
        {
            if (!(obj is View.Login login))
            {
                return;
            }

            string userId = iLogin.Validate(userName, login.passwordBox.Password);

            login.DialogResult = true;
            Globals.CurrentUserId = userId;
            login.Close();
        }
    }
}