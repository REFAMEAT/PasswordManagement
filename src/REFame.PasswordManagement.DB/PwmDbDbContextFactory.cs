using System;
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
            DatabaseData config = configuration
                .SetPath()
                .Create()
                .Load();


            var contextOptions = new DbContextOptionsBuilder<Context>();

            switch (config.Type)
            {
                case DataBaseType.Mssql:
                    var connectionStringSql = sqlConnectionStringBuilder.Create();
                    contextOptions.UseSqlServer(connectionStringSql);
                    break;
                case DataBaseType.SqLite:
                    var connectionStringSqLite = sqLiteConnectionStringBuilder.Create();
                    contextOptions.UseSqlite(connectionStringSqLite);
                    break;

                case DataBaseType.AccessDatabase:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var context = new Context(contextOptions.Options);
            return new PwmDbContext(context);
        }
    }
}