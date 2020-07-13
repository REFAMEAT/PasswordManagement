using System;
using System.Windows.Input;
using PasswordManagement.Backend.BinarySerializer;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class LoginViewModel : NotifyPropertyChanged
    {
        private string userName;
        private ICommand buttonCommandLogin;
        private BinaryData binData;

        public LoginViewModel()
        {
            BinaryHelper binHelper = new BinaryHelper();

            try
            {
                binData = binHelper.GetData();
            }
            catch (Exception)
            {
                // TODO wird durch erstmalige Anmeldung ersetzt
                binData = new BinaryData("firstUser", "PASSWORD");
                binHelper.Write(binData);
            }
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ICommand ButtonCommandLogin => buttonCommandLogin ??= new Command(DoLogin);

        private void DoLogin(object obj)
        {
            if (!(obj is Login login))
            {
                return;
            }

            if (binData.Validate(userName, login.passwordBox.Password))
            {
                login.DialogResult = true;
                login.Close();
            }

        }
    }
}