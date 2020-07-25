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
    internal class BinaryData
    {
        /// <summary>
        /// Create an existing user
        /// </summary>
        /// <param name="existing"></param>
        internal BinaryData(USERDATA existing)
        {
            UserNameHash = existing.USUSERNAME;
            PasswordHash = existing.USPASSWORD;
            Salt = existing.USSALT;

            Passwords = new List<PasswordData>();
        }

        /// <summary>
        /// Hashed Username
        /// </summary>
        internal string UserNameHash { get; }

        /// <summary>
        /// Hashed Password
        /// </summary>
        internal string PasswordHash { get; }

        /// <summary>
        /// Salt of the Password
        /// </summary>
        internal string Salt { get; set; }

        /// <summary>
        /// All Stored Passwords of a User
        /// </summary>
        internal List<PasswordData> Passwords { get; set; }
    }
}