using System.Windows.Input;
using System.Windows.Media;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Backend.Login;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class LoginViewModel : WpfBase.BindableBase
    {
        private readonly ILogin iLogin;
        private ICommand buttonCommandLogin;
        private string userName;

        public LoginViewModel(ILogin logonMethod)
        {
            iLogin = logonMethod;
            iLogin.Initialize();

            // Fallback, if logon Method doesn't work
            if (!iLogin.InitSuccessful)
            {
                iLogin = new LocalLogin();
            }

            bool needFirstUser = iLogin.NeedFirstUser();

            if (!needFirstUser)
            {
                return;
            }

            USERDATA firstUser = AddUser.CreateUser(true);

            if (firstUser != null && Globals.UseDatabase)
            {
                var data = new DataSet<USERDATA>();
                data.Entities.Add(firstUser);
                data.SaveChanges();
            }
            else if (firstUser != null)
            {
                new BinaryHelper().Write(new BinaryData(firstUser.USUSERNAME, firstUser.USPASSWORD, firstUser.USSALT));
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

            string userId = iLogin.Validate(userName, login.passwordBox.Password);

            if (userId != null)
            {
                login.DialogResult = true;
                Globals.CurrentUserId = userId;
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