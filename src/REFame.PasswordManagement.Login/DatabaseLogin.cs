using System.Linq;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login
{
    public class DatabaseLogin : ILogin
    {
        private readonly IPwmDbContext db;

        public DatabaseLogin(
            IPwmDbContextFactory dbContextFactory)
        {
            this.db = dbContextFactory.Create();
        }

        public void Dispose()
        {
        }

        /// <summary>
        ///     Validate the users Credentials
        /// </summary>
        /// <param name="userName">The clear text user name</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Validate(string userName, string password)
        {
            foreach (USERDATA user in db.USERDATA)
            {
                if (Encryption.DecryptString(user.USUSERNAME) == userName
                    && Password.GetHash(password + user.USSALT) == user.USPASSWORD)
                {
                    PWCore.CurrentCore.RegisterSingleton(new UserInfo(UserFactory.CreateUser(user)) as IUserInfo);
                    return user.USID;
                }
            }

            return null;
        }

        /// <summary>
        ///     Check if there is any user in the the Database
        /// </summary>
        /// <returns>true: if there is no user</returns>
        public bool NeedFirstUser()
        {
            try
            {
                return !db.USERDATA.ToList().Any();
            }
            catch (System.Exception)
            {
                return true;
            }
        }
    }
}