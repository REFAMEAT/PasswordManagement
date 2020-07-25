using System;
using PasswordManagement.Backend.Binary;
using PasswordManagement.Backend.Security;

namespace PasswordManagement.Backend.Login
{
    public class LocalLogin : ILogin
    {
        BinaryHelper helper = new BinaryHelper();

        public void Dispose()
        {
            helper = null;
        }

        public string Validate(string userName, string password)
        {
            BinaryData data = helper.GetData();

            if (Password.GetHash(userName + data.Salt) == data.UserNameHash
                && Password.GetHash(password + data.Salt) == data.PasswordHash)
            {
                return data.UserNameHash;
            }

            return null;
        }

        public bool NeedFirstUser()
        {
            try
            {
                helper.GetData();
            }
            catch (Exception)
            {
                return true;
            }

            return false;
        }

    }
}