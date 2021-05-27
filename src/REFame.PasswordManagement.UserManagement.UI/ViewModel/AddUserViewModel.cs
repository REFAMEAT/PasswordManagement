using System.Threading.Tasks;
using System.Windows.Input;
using REFame.PasswordManagement.UserManagement.UI.View;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.UserManagement.UI.ViewModel
{
    public class AddUserViewModel : BindableBase
    {
        private ICommand buttonCommandCreateUser;
        private bool inputOk;
        private string userName;
        private string title;
        private string name;
        private string eMail;

        public bool InputOk
        {
            get => inputOk;
            set => SetProperty(ref inputOk, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string EMail
        {
            get => eMail;
            set => SetProperty(ref eMail, value);
        }

        public ICommand ButtonCommandCreateUser => buttonCommandCreateUser ??= new AsyncCommand(DoCreateUser);

        private async Task DoCreateUser(object obj)
        {
            if (!(obj is AddUserView login) || !InputOk)
            {
                return;
            }

            login.DialogResult = true;
            login.Close();
        }
    }
}