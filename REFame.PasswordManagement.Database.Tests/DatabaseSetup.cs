using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;

namespace REFame.PasswordManagement.Database.Tests
{
    [SetUpFixture]
    public class DatabaseSetup
    {
        private static readonly string instanceName;

        protected Action<DbContextOptionsBuilder> options = x => x.UseInMemoryDatabase(new SqlConnectionStringBuilder
        {
            DataSource = instanceName,
            InitialCatalog = "TESTDATABASE",
            IntegratedSecurity = true
        }.ToString());

        static DatabaseSetup()
        {
            string path = Path.Combine(Path.GetTempPath(), "PasswordManagementUnitTestDatabase.txt");

            if (System.IO.File.Exists(path))
            {
                instanceName = System.IO.File.ReadAllText(path);
            }
            else
            {
                System.IO.File.Create(path).Dispose();
                System.IO.File.WriteAllText(path, "localhost");
                instanceName = "localhost";
            }
        }

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