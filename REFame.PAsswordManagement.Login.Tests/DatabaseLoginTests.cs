using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login.Tests
{
    [TestFixture(TestOf = typeof(DatabaseLogin))]
    public class DatabaseLoginTests
    {
        private DatabaseLogin login;
        private DataSet<USERDATA> dataSet;

        [SetUp()]
        public void Setup()
        {
            dataSet = new DataSet<USERDATA>(x
                => x.UseInMemoryDatabase("DatabaseLogin"));
            
            dataSet.Entities.AddRange(new List<USERDATA>()
            {
                 UserFactory.CreateUser("user1", "password1"),
                 UserFactory.CreateUser("user2", "password2"),
                 UserFactory.CreateUser("user3", "password3"),
                 UserFactory.CreateUser("user4", "password4"),
                 UserFactory.CreateUser("user5", "password5"),
                 UserFactory.CreateUser("user6", "password6"),
            });

            dataSet.SaveChanges();

            login = new DatabaseLogin(dataSet);
        }

        [Test()]
        public void DatabaseLoginTest()
        {
            var login = new DatabaseLogin(dataSet);

            Assert.That(() => new DatabaseLogin(dataSet), Throws.Nothing);
            Assert.That(login, Is.Not.Null);
        }

        [Test()]
        public void DisposeTest()
        {
            login.Dispose();

            Assert.That(() => login.Validate("dummy", "dummy"), 
                Throws.Exception.TypeOf(typeof(ObjectDisposedException)));
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

            var dataSet = new DataSet<USERDATA>(
                x => x.UseInMemoryDatabase("DatabaseLogin"));

            dataSet.Entities.RemoveRange(dataSet.Entities);
            dataSet.SaveChangesAsync();

            Assert.That(login.NeedFirstUser(), Is.True);
        }

        [Test()]
        public void InitializeTest()
        {
            Assert.That(() => login.Initialize(), Throws.Nothing);
        }
    }
}