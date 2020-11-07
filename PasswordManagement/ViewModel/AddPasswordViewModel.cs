using System.Windows.Input;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.App.ViewModel.Base;
using REFame.PasswordManagement.Backend.Security;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class AddPasswordViewModel : NotifyPropertyChanged
    {
        private ICommand buttonCommandAddPassword;
        private string comment;
        private string description;

        /// <summary>
        ///     The created Item, already Hashed/Salted
        /// </summary>
        public PasswordData NewItem { get; set; }

        /// <summary>
        ///     The Description of the password
        /// </summary>
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        /// <summary>
        ///     Optional Comments for the password
        /// </summary>
        public string Comment
        {
            get => comment;
            set => SetProperty(ref comment, value);
        }

        public ICommand ButtonCommandApply => buttonCommandAddPassword ??= new Command(DoApply);

        /// <summary>
        ///     Create the Data and Close Window
        /// </summary>
        /// <param name="obj">The <see cref="AddPassword" /> to close</param>
        private void DoApply(object obj)
        {
            if (!(obj is AddPassword window))
            {
                return;
            }

            var data = new PasswordData
            {
                Comments = Comment,
                Password = Encryption.EncryptString(window.password.Password, App.loginPw),
                Description = Description
            };

            window.Canceled = false;
            NewItem = data;
            window.Close();
        }
    }
}