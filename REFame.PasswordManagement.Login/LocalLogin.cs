using System;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login
{
    public class LocalLogin : ILogin
    {
        private BinaryHelper helper;

        public LocalLogin() : this(null)
        {
            
        }

        public LocalLogin(BinaryHelper helper)
        {
            this.helper = helper ?? new BinaryHelper();
        }

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

        public bool InitSuccessful { get; set; }

        public void Initialize()
        {
            InitSuccessful = true;
        }
    }
}