using System;

namespace PasswordManagement.Model.Interfaces
{
    internal interface ILogin : IDisposable
    {
        #nullable enable
        string Validate(string userName, string password);
        bool NeedFirstUser();
    }
}