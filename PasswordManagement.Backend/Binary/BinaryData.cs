using PasswordManagement.Backend.Security;
using System;
using System.Collections.Generic;

namespace PasswordManagement.Backend.BinarySerializer
{
    [Serializable]
    public class BinaryData
    {
        public BinaryData(string userName, string password)
        {
            Salt = Password.GetSalt();
            Passwords = new List<PasswordData>();

            
            UserNameHash = Password.GetHash(userName + Salt);
            PasswordHash = Password.GetHash(password + Salt);
        }

        public string UserNameHash { get; }
        public string PasswordHash { get; }
        public string Salt { get; set; }
        public List<PasswordData> Passwords { get; set; }

        public bool Validate(string userName, string password)
        {
            return Password.GetHash(userName + Salt) == UserNameHash 
                   && Password.GetHash(password + Salt) == PasswordHash;
        }
    }
}