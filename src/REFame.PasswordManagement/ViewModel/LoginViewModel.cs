using System;
using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private readonly ILogin iLogin;
        private readonly Action onWrongPassword;
        private ICommand buttonCommandLogin;
        private string userName;

        public LoginViewModel(ILogin logonMethod, Action onWrongPassword = null)
        {
            iLogin = logonMethod;
            this.onWrongPassword = onWrongPassword;
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ICommand ButtonCommandLogin => buttonCommandLogin ??= new AsyncCommand(DoLogin);

        private async Task DoLogin(object obj)
        {
            if (!(obj is View.Login login))
            {
                return;
            }

            string userId = iLogin.Validate(userName, login.passwordBox.Password);

            if (userId != null)
            {
                login.DialogResult = true;  
                login.Close(); 
            }
            else
            {
                onWrongPassword?.Invoke();
            }
        }
    }
}