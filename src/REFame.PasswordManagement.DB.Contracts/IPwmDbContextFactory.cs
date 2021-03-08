namespace REFame.PasswordManagement.DB.Contracts
{
    public interface IPwmDbContextFactory
    {
        IPwmDbContext Create();
    }
}