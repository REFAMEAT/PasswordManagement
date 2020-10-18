using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Database.Tests
{
    [SetUpFixture]
    public class DatabaseSetup
    {
        protected Action<DbContextOptionsBuilder> options = x => x.UseSqlServer(new SqlConnectionStringBuilder
        {
            DataSource = "localhost",
            InitialCatalog = "TESTDATABASE",
            IntegratedSecurity = true
        }.ToString());

        [OneTimeSetUp]
        public void CreateDatabase()
        {
            var data = new DataSet<PASSWORDDATA>(options);

            data.Database.EnsureCreated();
        }

        [OneTimeTearDown]
        public void DeleteDatabase()
        {
            var data = new DataSet<PASSWORDDATA>(options);

            data.Database.EnsureDeleted();
        }
    }
}