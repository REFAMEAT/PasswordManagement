namespace REFame.PasswordManagement.Configuration.Contracts
{
    public interface IConfigurationFactory<T> where T : new()
    {
        IConfigurationFactory<T> SetPath(string path = null);
        IConfiguration<T> Create();
        string CurrentPath { get; }
    }
}