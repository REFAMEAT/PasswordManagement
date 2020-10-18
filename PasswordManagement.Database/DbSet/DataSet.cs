using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PasswordManagement.File.Config;
using PasswordManagement.Model.Setting;

namespace PasswordManagement.Database.DbSet
{
    public class DataSet<TEntity> : DbContext where TEntity : class
    {
        private readonly DatabaseData config;
        private readonly Action<DbContextOptionsBuilder> onConfiguringAction;

        public DataSet(DatabaseData config = null)
        {
            this.config = config ?? JsonHelper<DatabaseData>.GetData();
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