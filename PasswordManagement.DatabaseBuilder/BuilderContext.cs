using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace REFame.PasswordManagement.DatabaseBuilder
{
    public class BuilderContext<T> : DbContext where T : class
    {
        private readonly Assembly[] assemblies;
        private readonly SqlConnectionStringBuilder builder;

        public BuilderContext(Assembly[] assemblies, string serverName, string databaseName, string userName,
            string password)
        {
            builder = new SqlConnectionStringBuilder
            {
                InitialCatalog = databaseName,
                DataSource = serverName,
                UserID = userName,
                Password = password
            };

            this.assemblies = assemblies;
        }

        public BuilderContext(Assembly[] assemblies, string serverName, string databaseName)
        {
            builder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = true
            };

            this.assemblies = assemblies;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(builder.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tableBuilder = new TableBuilder();
            List<Type> models = tableBuilder.GetModels<T>(assemblies);
            tableBuilder.BuildTables(models, modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}