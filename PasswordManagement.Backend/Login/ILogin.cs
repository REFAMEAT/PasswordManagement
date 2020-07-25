using System;

namespace PasswordManagement.Backend.Login
{
    public interface ILogin : IDisposable
    {
        #nullable enable
        string Validate(string userName, string password);
        bool NeedFirstUser();
    }
}