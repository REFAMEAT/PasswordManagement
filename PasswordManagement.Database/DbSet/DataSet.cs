using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PasswordManagement.File;

namespace PasswordManagement.Database.DbSet
{
    internal class DataSet<TEntity> : DbContext where TEntity : class
    {
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

        internal DbSet<TEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
    }
}