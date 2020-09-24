using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;

namespace PasswordManagement.Database.Tests
{
    [SetUpFixture]
    public class DatabaseSetup
    {
        [OneTimeSetUp]
        public void CreateDatabase()
        {
            var data = new DataSet<PASSWORDDATA>(x => x.UseInMemoryDatabase("InMemoryTestDatabase"));

            data.Database.EnsureCreatedAsync();
        }

        [OneTimeTearDown]
        public void DeleteDatabase()
        {
            var data = new DataSet<PASSWORDDATA>(x => x.UseInMemoryDatabase("InMemoryTestDatabase"));

            data.Database.EnsureDeleted();
        }
    }
}