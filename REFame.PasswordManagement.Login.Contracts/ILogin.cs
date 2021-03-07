using System;

namespace REFame.PasswordManagement.Login.Contracts
{
    public interface ILogin : IDisposable
    {
        string Validate(string userName, string password);
        bool NeedFirstUser();
        bool InitSuccessful { get; set; }
        void Initialize();
    }
}