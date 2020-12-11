using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using REFame.PasswordManagement.File.Config;
using REFame.PasswordManagement.File.Contracts.Config;
using REFame.PasswordManagement.Model.Setting;

namespace REFame.PasswordManagement.Database.DbSet
{
    public class DataSet<TEntity> : DbContext, IDataSet<TEntity> where TEntity : class
    {
        private readonly DatabaseData config;
        private readonly Action<DbContextOptionsBuilder> onConfiguringAction;

        public DataSet(IConfigurationFactory<DatabaseData> factory)
        {
            config = factory
                .SetPath()
                .Create()
                .Load();
        }

        public DataSet(Action<DbContextOptionsBuilder> onConfiguringAction)
        {
            this.onConfiguringAction = onConfiguringAction;
        }

        public DbSet<TEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!(config is null))
            {
                var connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = config.ServerName ??= "",
                    InitialCatalog = config.DatabaseName ??= "",
                    IntegratedSecurity = config.IntegratedSecurity,
                    UserID = config.Username ??= "",
                    Password = config.Password ??= ""
                }.ToString();

                optionsBuilder.UseSqlServer(connectionString);

            }
            else
            {
                onConfiguringAction(optionsBuilder);
            }
        }
    }
}