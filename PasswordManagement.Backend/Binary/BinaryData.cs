using System;
using System.Collections.Generic;

namespace PasswordManagement.Backend.BinarySerializer
{
    [Serializable]
    public class BinaryData
    {
        public BinaryData(string userName, string password)
        {
            Salt = Security.GetSalt();
            Passwords = new List<PasswordData>();

            
            UserNameHash = Security.GetHash(userName + Salt);
            PasswordHash = Security.GetHash(password + Salt);
        }

        public string UserNameHash { get; }
        public string PasswordHash { get; }
        public string Salt { get; set; }
        public List<PasswordData> Passwords { get; set; }

        public bool Validate(string userName, string password)
        {
            return Security.GetHash(userName + Salt) == UserNameHash 
                   && Security.GetHash(password + Salt) == PasswordHash;
        }
    }
}