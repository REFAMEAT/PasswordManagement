using PasswordManagement.Backend.BinarySerializer;
using PasswordManagement.Backend.Security;
using PasswordManagement.View;
using PasswordManagement.ViewModel.Base;
using System.Windows.Input;

namespace PasswordManagement.ViewModel
{
    public class AddPasswordViewModel : NotifyPropertyChanged
    {
        private string comment;
        private string description;
        private ICommand buttonCommandAddPassword;

        public PasswordData NewItem { get; set; }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Comment
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }

        public ICommand ButtonCommandApply => buttonCommandAddPassword ??= new Command(DoApply);

        private void DoApply(object obj)
        {
            if (!(obj is AddPassword window))
            {
                return;
            }

            PasswordData data = new PasswordData()
            {
                Comments = Comment,
                Password = Encryption.EncryptString(window.password.Password, App.LogedIn)
            };

            var x = Encryption.DecryptString(data.Password, App.LogedIn);
        }
    }
}