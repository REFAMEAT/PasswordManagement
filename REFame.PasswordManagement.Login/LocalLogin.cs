using System;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.File.Binary.Factory;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Model.Interfaces;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login
{
    public class LocalLogin : ILogin
    {
        private IBinaryHelper helper;
        
        public LocalLogin(IBinaryHelperFactory helper)
        {
            if (string.IsNullOrWhiteSpace(helper.CurrentPath))
            {
                this.helper = helper
                       .SetPath()
                       .Create(); 
            }
            else
            {
                this.helper = helper.Create();
            }
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