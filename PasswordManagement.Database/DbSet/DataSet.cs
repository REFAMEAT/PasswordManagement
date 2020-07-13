using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PasswordManagement.Database.DbSet
{
    public class DataSet<TEntity> : DbContext where TEntity : class
    {
        public DbSet<TEntity> Entities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = "PasswordManagement",
                IntegratedSecurity = true
            }.ToString());
        }
    }
}