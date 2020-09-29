﻿using System;
using PasswordManagement.Backend.Security;
using PasswordManagement.File.Binary;
using PasswordManagement.Model;
using PasswordManagement.Model.Interfaces;

namespace PasswordManagement.Backend.Login
{
    internal class LocalLogin : ILogin
    {
        private BinaryHelper helper;

        internal LocalLogin()
        {
            helper = new BinaryHelper();
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
                return data.UserNameHash;

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