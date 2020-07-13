using System.Windows.Input;
using PasswordManagement.Backend.BinarySerializer;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;

namespace PasswordManagement.ViewModel
{
    public class AddUserViewModel : NotifyPropertyChanged
    {
        private string userName;
        private ICommand buttonCommandCreateUser;
        private bool inputOk;

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
            if (!(obj is AddUser login))
            {
                return;
            }
        }
    }
}