using REFame.PasswordManagement.AppCore.Contracts.Components;

namespace REFame.PasswordManagement.AppCore.Tests.Mock
{
    internal class MockLoginHandler : ILoginHandler
    {
        public bool CreateFirstUserIfNeeded()
        {
            return true;
        }

        public bool Login()
        {
            return true;
        }
    }
}