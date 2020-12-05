using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class AddUserViewModel : BindableBase
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

        public ICommand ButtonCommandCreateUser => buttonCommandCreateUser ??= new AsyncCommand(DoCreateUser);

        private async Task DoCreateUser(object obj)
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