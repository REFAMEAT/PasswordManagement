using System;
using System.Windows.Input;
using System.Windows.Media;
using PasswordManagement.Backend.Binary;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class LoginViewModel : NotifyPropertyChanged
    {
        private readonly BinaryData binData;
        private ICommand buttonCommandLogin;
        private string userName;

        public LoginViewModel()
        {
            BinaryHelper binHelper = new BinaryHelper();

            try
            {
                binData = binHelper.GetData();
            }
            catch (Exception)
            {
                var firstUser = AddUser.CreateUser(true);
                if (firstUser != null)
                {
                    binHelper.Write(new BinaryData(firstUser));

                    binData = binHelper.GetData(); 
                }
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
            if (!(obj is Login login)) return;

            if (binData.Validate(userName, login.passwordBox.Password))
            {
                login.DialogResult = true;
                login.Close();
            }
            else
            {
                login.userNameTextBox.Foreground = Brushes.Red;
                login.passwordBox.Foreground = Brushes.Red;
            }
        }
    }
}