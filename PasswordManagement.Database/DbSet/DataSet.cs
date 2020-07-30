using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PasswordManagement.File.Config;
using PasswordManagement.Model.Setting;
using System;

namespace PasswordManagement.Database.DbSet
{
    internal class DataSet<TEntity> : DbContext where TEntity : class
    {
        private readonly Action<DbContextOptionsBuilder> onConfiguringAction;
        private readonly DatabaseData config;

        internal DataSet(DatabaseData config = null)
        {
            if (config == null)
            {
                this.config = JsonHelper<DatabaseData>.GetData();
            }
            else
            {
                this.config = config;   
            }
        }

        internal DataSet(Action<DbContextOptionsBuilder> onConfiguringAction)
        {
            this.onConfiguringAction = onConfiguringAction;
        }

        internal DbSet<TEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (config is null)
            {
                string connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = config.ServerName ??= "",
                    InitialCatalog = config.DatabaseName ??= "",
                    IntegratedSecurity = config.IntegratedSecurity,
                    UserID = config.Username ??= "",
                    Password = config.Password  ??= ""
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