using System;
using System.Collections.Generic;

namespace REFame.PasswordManagement.Model
{
    /// <summary>
    ///     Datas to Save in a Binary File
    /// </summary>
    [Serializable]
    public class BinaryData
    {
        public BinaryData(string userName, string password, string salt)
        {
            UserNameHash = userName;
            PasswordHash = password;
            Salt = salt;

            Passwords = new List<PasswordData>();
        }

        /// <summary>
        ///     Hashed Username
        /// </summary>
        public string UserNameHash { get; }

        /// <summary>
        ///     Hashed Password
        /// </summary>
        public string PasswordHash { get; }

        /// <summary>
        ///     Salt of the Password
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        ///     All Stored Passwords of a User
        /// </summary>
        public List<PasswordData> Passwords { get; set; }
    }
}