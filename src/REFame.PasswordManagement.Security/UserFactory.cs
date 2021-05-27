using System;
using System.Threading;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;

namespace REFame.PasswordManagement.Security
{
    /// <summary>
    ///     Creates users and hashed passwords
    /// </summary>
    public class UserFactory
    {
        public static User CreateUser(USERDATA user)
        {
            return new User()
            {
                Identifier = user.USID,
                FullName = DecryptNeeded(user.USNAME),
                UserName = DecryptNeeded(user.USUSERNAME),

                EMail = DecryptOptional(user.USMAIL),
            };
        }

        public static USERDATA CreateUser(User user, string password)
        {
            string pwSalt = Password.GetSalt();

            var newUser = new USERDATA
            {
                USID = Guid.NewGuid().ToString(),
                USNAME = EncryptNeeded(user.FullName),
                USUSERNAME = EncryptNeeded(user.UserName),

                USPASSWORD = Password.GetHash(password + pwSalt),

                USMAIL = EncryptOptional(user.EMail),
                USSALT = pwSalt
            };

            // Thread sleeps for few seconds for preventing brute-force attacks
            Thread.Sleep(500);

            return newUser;
        }

        private static string DecryptNeeded(string value)
        {
            return Encryption.DecryptString(value);
        }

        private static string DecryptOptional(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return Encryption.DecryptString(value);
        }

        private static string EncryptOptional(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return Encryption.EncryptString(value);
        }

        private static string EncryptNeeded(string value)
        {
            return Encryption.EncryptString(value);
        }
    }
}