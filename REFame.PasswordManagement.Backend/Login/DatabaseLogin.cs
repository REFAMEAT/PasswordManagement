using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.Backend.Security;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Logging;
using REFame.PasswordManagement.Model.Interfaces;

namespace REFame.PasswordManagement.Backend.Login
{
    public class DatabaseLogin : ILogin
    {
        private readonly DataSet<USERDATA> userdatas;

        public DatabaseLogin(DataSet<USERDATA> dataSet = null)
        {
            userdatas = dataSet ?? new DataSet<USERDATA>();
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
            try
            {
                userdatas.Database.OpenConnection();
                userdatas.Database.CloseConnection();

                InitSuccessful = true;
                Globals.UseDatabase = true;
            }
            catch (Exception ex)
            {
                InitSuccessful = false;
                Logger.Current.Get().Error(ex);
                Globals.UseDatabase = false;
            }
        }
    }
}