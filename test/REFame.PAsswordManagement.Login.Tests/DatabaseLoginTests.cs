using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.DB;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.DB.Internals;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login.Tests
{
    [TestFixture(TestOf = typeof(DatabaseLogin))]
    public class DatabaseLoginTests
    {
        private DatabaseLogin login;
        private DataSet<USERDATA> dataSet;
        private Mock<IPwmDbContextFactory> factoryMock;

        [SetUp()]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>();
            options.UseInMemoryDatabase("PWMTESTDB");

            PwmDbContext db = new PwmDbContext(new Context(options.Options));
            
            db.USERDATA.AddRange(new List<USERDATA>()
            {
                 UserFactory.CreateUser("user1", "password1"),
                 UserFactory.CreateUser("user2", "password2"),
                 UserFactory.CreateUser("user3", "password3"),
                 UserFactory.CreateUser("user4", "password4"),
                 UserFactory.CreateUser("user5", "password5"),
                 UserFactory.CreateUser("user6", "password6"),
            });

            db.SaveChanges();


            factoryMock = new Mock<IPwmDbContextFactory>();
            factoryMock
                .Setup(x => x.Create())
                .Returns(db);

            login = new DatabaseLogin(factoryMock.Object);
        }

        [Test()]
        public void DatabaseLoginTest()
        {
            var login = new DatabaseLogin(factoryMock.Object);

            Assert.That(() => new DatabaseLogin(factoryMock.Object), Throws.Nothing);
            Assert.That(login, Is.Not.Null);
        }

        [Test()]
        public void ValidateTest()
        {
            string userIdentifier = login.Validate("user1", "password1");

            Assert.That(userIdentifier, Is.Not.Empty);
        }

        [Test()]
        public void NeedFirstUserTest()
        {
            Assert.That(login.NeedFirstUser(), Is.False);

            var options = new DbContextOptionsBuilder<Context>();
            options.UseInMemoryDatabase("PWMTESTDB");

            PwmDbContext db = new PwmDbContext(new Context(options.Options));


            db.USERDATA.RemoveRange(db.USERDATA);
            db.SaveChangesAsync();

            Assert.That(login.NeedFirstUser(), Is.True);
        }

        [Test()]
        public void InitializeTest()
        {
            Assert.That(() => login.Initialize(), Throws.Nothing);
        }
    }
}