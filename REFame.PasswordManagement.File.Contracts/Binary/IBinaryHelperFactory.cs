namespace REFame.PasswordManagement.File.Contracts.Binary
{
    public interface IBinaryHelperFactory
    {
        IBinaryHelperFactory SetPath(string path = null);
        IBinaryHelper Create();
        string CurrentPath { get; }
    }
}