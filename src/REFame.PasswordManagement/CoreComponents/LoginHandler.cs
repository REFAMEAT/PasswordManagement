using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Contracts.Components;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;
using REFame.PasswordManagement.UserManagement.Contracts;

namespace REFame.PasswordManagement.App.CoreComponents
{
    public class LoginHandler : ILoginHandler
    {
        private readonly ILogin login;
        private readonly IPwmDbContext db;
        private readonly INewUserService newUserService;

        public LoginHandler(
            IPwmDbContextFactory factory, 
            ILogin login, 
            INewUserService newUserService)
        {
            this.login = login;
            this.newUserService = newUserService;
            this.db = factory.Create();
        }

        public bool CreateFirstUserIfNeeded()
        {
            if (!login.NeedFirstUser())
            {
                return true;
            }
            
            (User newUser, var newUserPassword) = newUserService.Create();

            if (newUser == null)
            {
                return false;
            }

            USERDATA firstUser = UserFactory.CreateUser(newUser, newUserPassword);

            db.USERDATA.Add(firstUser);
            db.SaveChanges();
            return true;
        }

        public bool Login()
        {
            return WpfCore.Current.Login(PWCore.CurrentCore);
        }
    }
}