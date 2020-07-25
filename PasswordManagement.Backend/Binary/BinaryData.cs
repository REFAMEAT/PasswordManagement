using System;
using System.Collections.Generic;
using PasswordManagement.Backend.Security;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Backend.Binary
{
    /// <summary>
    /// Datas to Save in a Binary File
    /// </summary>
    [Serializable]
    public class BinaryData
    {
        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public BinaryData(string userName, string password)
        {
            Salt = Password.GetSalt();
            Passwords = new List<PasswordData>();


            UserNameHash = Password.GetHash(userName + Salt);
            PasswordHash = Password.GetHash(password + Salt);

            Passwords = new List<PasswordData>();
        }

        /// <summary>
        /// Create an existing user
        /// </summary>
        /// <param name="existing"></param>
        public BinaryData(USERDATA existing)
        {
            UserNameHash = existing.USUSERNAME;
            PasswordHash = existing.USPASSWORD;
            Salt = existing.USSALT;

            Passwords = new List<PasswordData>();
        }

        /// <summary>
        /// Hashed Username
        /// </summary>
        public string UserNameHash { get; }

        /// <summary>
        /// Hashed Password
        /// </summary>
        public string PasswordHash { get; }

        /// <summary>
        /// Salt of the Password
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// All Stored Passwords of a User
        /// </summary>
        public List<PasswordData> Passwords { get; set; }
    }
}