using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.DB;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Internals;

[assembly: InternalsVisibleTo("REFame.PasswordManagement.Data.Tests")]

namespace REFame.PasswordManagement.TestBase
{
    public class DatabaseSetup
    {
        protected readonly IPwmDbContextFactory dbContextFactory;
        private readonly DbContextOptions<Context> dbContextOptions;

        protected DatabaseSetup()
        {
            var contextOptions = new DbContextOptionsBuilder<Context>();
            contextOptions.UseInMemoryDatabase("PWMTESTDATABASE");
            dbContextOptions = contextOptions.Options;

            IPwmDbContext db = new PwmDbContext(new Context(dbContextOptions));

            var dbContextFactoryMock = new Mock<IPwmDbContextFactory>();
            dbContextFactoryMock
                .Setup(x => x.Create())
                .Returns(db);

            dbContextFactory = dbContextFactoryMock.Object;
        }

        [OneTimeSetUp]
        public void CreateDatabase()
        {
            var context = new Context(dbContextOptions);

            if (!context.Database.EnsureCreated())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [OneTimeTearDown]
        public void DeleteDatabase()
        {
            new Context(dbContextOptions).Database.EnsureDeleted();
        }
    }
}