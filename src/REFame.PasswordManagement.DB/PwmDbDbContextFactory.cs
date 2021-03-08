using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.Configuration.Contracts;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Internals;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.DB
{
    public class PwmDbDbContextFactory : IPwmDbContextFactory
    {
        private readonly ISqlConnectionStringBuilder sqlConnectionStringBuilder;
        private readonly ISqLiteConnectionStringBuilder sqLiteConnectionStringBuilder;
        private readonly IConfigurationFactory<DatabaseData> configuration;

        public PwmDbDbContextFactory(
            ISqlConnectionStringBuilder sqlConnectionStringBuilder,
            ISqLiteConnectionStringBuilder sqLiteConnectionStringBuilder,
            IConfigurationFactory<DatabaseData> configuration)
        {
            this.sqlConnectionStringBuilder = sqlConnectionStringBuilder;
            this.sqLiteConnectionStringBuilder = sqLiteConnectionStringBuilder;
            this.configuration = configuration;
        }

        public IPwmDbContext Create()
        {
            var config = configuration
                .SetPath()
                .Create()
                .Load();

            
            var contextOptions = new DbContextOptionsBuilder<Context>();

            if (config.UseDatabase)
            {
                var connectionString = sqlConnectionStringBuilder.Create();
                contextOptions.UseSqlServer(connectionString);
            }
            else
            {
                var connectionString = sqLiteConnectionStringBuilder.Create();
                contextOptions.UseSqlite(connectionString);
            }

            var context = new Context(contextOptions.Options);
            return new PwmDbContext(context);
        }
    }
}