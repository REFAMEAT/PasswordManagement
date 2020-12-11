using System.Windows.Input;
using REFame.PasswordManagement.App.View;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.WpfBase;

namespace REFame.PasswordManagement.App.ViewModel
{
    public class LoginViewModel : BindableBase
    {
        private readonly ILogin iLogin;
        private ICommand buttonCommandLogin;
        private string userName;

        public LoginViewModel(ILogin logonMethod)
        {
            iLogin = logonMethod;
            iLogin.Initialize();

            bool needFirstUser = iLogin.NeedFirstUser();

            if (!needFirstUser)
            {
                return;
            }

            USERDATA firstUser = AddUser.CreateUser(true);

            if (firstUser != null && Globals.UseDatabase)
            {
                var data = PWCore
                    .CurrentCore
                    .GetRegisteredType<IDataSet<USERDATA>>();
                data.Entities.Add(firstUser);
                data.SaveChanges();
            }
            else if (firstUser != null)
            {
                PWCore.CurrentCore
                    .GetRegisteredType<IBinaryHelperFactory>()
                    .SetPath()
                    .Create()
                    .Write(
                        new BinaryData(
                            firstUser.USUSERNAME,
                            firstUser.USPASSWORD,
                            firstUser.USSALT));
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
            if (!(obj is View.Login login))
            {
                return;
            }

            string userId = iLogin.Validate(userName, login.passwordBox.Password);

            login.DialogResult = true;
            Globals.CurrentUserId = userId;
            login.Close();
        }
    }
}