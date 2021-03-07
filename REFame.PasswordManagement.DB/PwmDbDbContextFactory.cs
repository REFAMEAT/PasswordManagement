using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Internals;

namespace REFame.PasswordManagement.DB
{
    public class PwmDbDbContextFactory : IPwmDbContextFactory
    {
        private readonly IConnectionStringBuilder connectionStringBuilder;

        public PwmDbDbContextFactory(
            IConnectionStringBuilder connectionStringBuilder)
        {
            this.connectionStringBuilder = connectionStringBuilder;
        }

        public IPwmDbContext Create()
        {
            var connectionString = connectionStringBuilder.Create();

            var contextOptions = new DbContextOptionsBuilder<Context>();
            contextOptions.UseSqlServer(connectionString);

            var context = new Context(contextOptions.Options);

            return new PwmDbContext(context);
        }
    }
}