using System;
using System.Threading;
using REFame.PasswordManagement.Database.Model;

namespace REFame.PasswordManagement.Backend.Security
{
    /// <summary>
    ///     Creates users and hashed passwords
    /// </summary>
    public class UserFactory
    {
        public static USERDATA CreateUser(string userName, string password)
        {
            string pwSalt = Password.GetSalt();

            var newUser = new USERDATA
            {
                USID = Guid.NewGuid().ToString(),
                USUSERNAME = Password.GetHash(userName + pwSalt),
                USPASSWORD = Password.GetHash(password + pwSalt),
                USSALT = pwSalt
            };

            // Thread sleeps for few seconds for preventing brute-force attacks
            Thread.Sleep(500);

            return newUser;
        }
    }
}