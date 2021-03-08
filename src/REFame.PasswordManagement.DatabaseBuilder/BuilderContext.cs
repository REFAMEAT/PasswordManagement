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

        public BuilderContext(Assembly[] assemblies, DbContextOptions options) : base(options)
        {
            this.assemblies = assemblies;
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