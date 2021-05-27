using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.AppCore.Contracts;
using REFame.PasswordManagement.DB;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.DB.Internals;
using REFame.PasswordManagement.Login.Contracts;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login.Tests
{
    [TestFixture(TestOf = typeof(DatabaseLogin))]
    public class DatabaseLoginTests
    {
        private DatabaseLogin login;
        private Mock<IPwmDbContextFactory> factoryMock;
        private Mock<IUserInfo> userInfoMock;

        [SetUp()]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<Context>();
            options.UseInMemoryDatabase("PWMTESTDB");

            PwmDbContext db = new PwmDbContext(new Context(options.Options));

            db.USERDATA.AddRange(new List<USERDATA>()
            {
                 UserFactory.CreateUser(new User() {UserName =  "user1", FullName = "fullName 1"}, "password1"),
                 UserFactory.CreateUser(new User() {UserName =  "user2", FullName = "fullName 2"}, "password2"),
                 UserFactory.CreateUser(new User() {UserName =  "user3", FullName = "fullName 3"}, "password3"),
                 UserFactory.CreateUser(new User() {UserName =  "user4", FullName = "fullName 4"}, "password4"),
                 UserFactory.CreateUser(new User() {UserName =  "user5", FullName = "fullName 5"}, "password5"),
                 UserFactory.CreateUser(new User() {UserName =  "user6", FullName = "fullName 6"}, "password6"),

            });

            db.SaveChanges();


            factoryMock = new Mock<IPwmDbContextFactory>();
            userInfoMock = new Mock<IUserInfo>();

            factoryMock
                .Setup(x => x.Create())
                .Returns(db);
            userInfoMock
                .SetupGet(mock => mock.User)
                .Returns(new User() {UserName = "user1"});

            login = new DatabaseLogin(factoryMock.Object);
        }

        [Test()]
        public void ValidateTest()
        {
            PWCore.CurrentCore = new Mock<ICore>().Object;
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
    }
}