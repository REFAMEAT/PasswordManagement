using System;
using System.Windows.Input;
using Microsoft.Cci;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Model.Interfaces;
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

            if (userId != null)
            {
                login.DialogResult = true;
                Globals.CurrentUserId = userId;
                login.Close(); 
            }
            else
            {
                onWrongPassword?.Invoke();
            }
        }
    }
}