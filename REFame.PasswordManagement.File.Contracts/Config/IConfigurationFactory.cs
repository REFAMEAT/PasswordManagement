namespace REFame.PasswordManagement.File.Contracts.Config
{
    public interface IConfigurationFactory<T> where T : new()
    {
        IConfigurationFactory<T> SetPath(string path = null);
        IConfiguration<T> Create();
        string CurrentPath { get; }
    }
}