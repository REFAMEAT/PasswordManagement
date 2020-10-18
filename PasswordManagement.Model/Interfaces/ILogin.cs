using System;

namespace PasswordManagement.Model.Interfaces
{
    public interface ILogin : IDisposable
    {
#nullable enable
        string Validate(string userName, string password);
        bool NeedFirstUser();
        bool InitSuccessful { get; set; }
        void Initialize();
    }
}