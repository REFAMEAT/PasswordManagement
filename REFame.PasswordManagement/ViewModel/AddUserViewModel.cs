using System.Windows.Input;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.App.ViewModel.Base;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class AddUserViewModel : NotifyPropertyChanged
    {
        private ICommand buttonCommandCreateUser;
        private bool inputOk;
        private string userName;

        public bool InputOk
        {
            get => inputOk;
            set => SetProperty(ref inputOk, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ICommand ButtonCommandCreateUser => buttonCommandCreateUser ??= new Command(DoCreateUser);

        private void DoCreateUser(object obj)
        {
            if (!(obj is AddUser login) || !InputOk)
            {
                return;
            }

            login.DialogResult = true;
            login.Close();
        }
    }
}