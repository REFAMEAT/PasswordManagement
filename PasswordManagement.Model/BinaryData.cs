using System;
using System.Collections.Generic;

namespace PasswordManagement.Model
{
    /// <summary>
    ///     Datas to Save in a Binary File
    /// </summary>
    [Serializable]
    internal class BinaryData
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
        internal string UserNameHash { get; }

        /// <summary>
        ///     Hashed Password
        /// </summary>
        internal string PasswordHash { get; }

        /// <summary>
        ///     Salt of the Password
        /// </summary>
        internal string Salt { get; set; }

        /// <summary>
        ///     All Stored Passwords of a User
        /// </summary>
        internal List<PasswordData> Passwords { get; set; }
    }
}