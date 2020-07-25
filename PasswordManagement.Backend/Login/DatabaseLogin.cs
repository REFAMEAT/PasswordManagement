using System.Linq;
using PasswordManagement.Backend.Security;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Backend.Login
{
    public class DatabaseLogin : ILogin
    {
        private readonly DataSet<USERDATA> userdatas;

        public DatabaseLogin()
        {
            userdatas = new DataSet<USERDATA>();
        }

        public void Dispose()
        {
            userdatas.Dispose();
        }

        public string Validate(string userName, string password)
        {
            foreach (USERDATA user in userdatas.Entities)
            {
                if (Password.GetHash(userName + user.USSALT) == user.USUSERNAME 
                    && Password.GetHash(password + user.USSALT) == user.USPASSWORD)
                {
                    return user.USID;
                }
            }

            return null;
        }

        public bool NeedFirstUser()
        {
            return !userdatas.Entities.ToList().Any();
        }
    }
}