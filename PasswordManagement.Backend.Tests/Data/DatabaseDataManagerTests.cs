using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using PasswordManagement.Backend.Data;
using PasswordManagement.Database.DbSet;
using PasswordManagement.Database.Model;
using PasswordManagement.Model;

namespace PasswordManagement.Backend.Tests.Data
{
    public class DatabaseDataManagerTests
    {
        private DataSet<PASSWORDDATA> dataSet;
        private DatabaseDataManager dataManager;

        [SetUp]
        public void Setup()
        {
            dataSet = new DataSet<PASSWORDDATA>(x => x.UseInMemoryDatabase("TestBackend"));
            dataSet.Database.EnsureCreated();
            dataManager = new DatabaseDataManager(null, dataSet);

            dataSet.Entities.AddRange(
                new PASSWORDDATA() { PWID = "Id1", PWCOMMENT = "test1", PWDATA = "safePassword1" },
                new PASSWORDDATA() { PWID = "Id2", PWCOMMENT = "test2", PWDATA = "safePassword2" },
                new PASSWORDDATA() { PWID = "Id3", PWCOMMENT = "test3", PWDATA = "safePassword3" },
                new PASSWORDDATA() { PWID = "Id4", PWCOMMENT = "test4", PWDATA = "safePassword4" },
                new PASSWORDDATA() { PWID = "Id5", PWCOMMENT = "test5", PWDATA = "safePassword5" },
                new PASSWORDDATA() { PWID = "Id6", PWCOMMENT = "test6", PWDATA = "safePassword6" },
                new PASSWORDDATA() { PWID = "Id7", PWCOMMENT = "test7", PWDATA = "safePassword7" },
                new PASSWORDDATA() { PWID = "Id8", PWCOMMENT = "test8", PWDATA = "safePassword8" },
                new PASSWORDDATA() { PWID = "Id9", PWCOMMENT = "test9", PWDATA = "safePassword9" }
                );

            dataSet.SaveChanges();
        }

        [Test]
        public void TestLoadData()
        {
            List<PasswordData> data = dataManager.LoadData();

            Assert.That(data.Count, Is.EqualTo(9));
            Assert.That(data.Find(x => x.Identifier == "Id1"), Is.Not.Null);

        }

        [TearDown]
        public void TearDown()
        {
            dataSet.Database.EnsureDeleted();
        }
    }
}