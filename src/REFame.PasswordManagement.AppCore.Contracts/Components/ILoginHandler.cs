namespace REFame.PasswordManagement.AppCore.Contracts.Components
{
    public interface ILoginHandler
    {
        bool CreateFirstUserIfNeeded();

        bool Login();
    }
}