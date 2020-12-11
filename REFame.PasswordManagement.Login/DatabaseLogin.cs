using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.Backend;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login
{
    public class DatabaseLogin : ILogin
    {
        private readonly IDataSet<USERDATA> userdatas;
        
        public DatabaseLogin(IDataSet<USERDATA> dataSet)
        {
            userdatas = dataSet;
        }

        public void Dispose()
        {
            userdatas.Dispose();
        }

        /// <summary>
        ///     Validate the users Credentials
        /// </summary>
        /// <param name="userName">The clear text user name</param>
        /// <param name="password"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Check if there is any user in the the Database
        /// </summary>
        /// <returns>true: if there is no user</returns>
        public bool NeedFirstUser()
        {
            return !userdatas.Entities.ToList().Any();
        }

        public bool InitSuccessful { get; set; }

        public void Initialize()
        {
            bool canConnect;

            try
            {
                canConnect = userdatas.Database.CanConnect();
            }
            catch (Exception e)
            {
                canConnect = false;
                Logger.Current.Get().Warning(e);
            }

            InitSuccessful = Globals.UseDatabase = canConnect;
        }
    }
}