using System.Linq;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login
{
    public class DatabaseLogin : ILogin
    {
        private IPwmDbContext db;

        public DatabaseLogin(IPwmDbContextFactory dbContextFactory)
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
                if (Password.GetHash(userName + user.USSALT) == user.USUSERNAME
                    && Password.GetHash(password + user.USSALT) == user.USPASSWORD)
                {
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
            return !db.USERDATA.ToList().Any();
        }

        public bool InitSuccessful { get; set; }

        public void Initialize()
        {
            InitSuccessful = true;
        }
    }
}