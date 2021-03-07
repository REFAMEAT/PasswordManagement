using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using REFame.PasswordManagement.Data.DataManager;
using REFame.PasswordManagement.Database.Tests;
using REFame.PasswordManagement.DB.Contracts;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Data.Tests.DataManager
{
    [TestFixture]
    public class DatabaseDataManagerTests : DatabaseSetup
    {
        [SetUp]
        public async Task Setup()
        {
            db = dbContextFactory.Create();
            dataManager = new DatabaseDataManager(dbContextFactory);

            db.PASSWORDDATA.AddRange(Data);

            await db.SaveChangesAsync();
        }

        private IPwmDbContext db;

        [TearDown]
        public void TearDown()
        {
            db.RemoveRange(db.PASSWORDDATA);
            db.SaveChanges();
        }

        private DatabaseDataManager dataManager;

        [Test]
        public void TestLoadData()
        {
            List<PasswordData> data = dataManager.LoadData();

            Assert.That(data.Count, Is.EqualTo(9));
            Assert.That(data.Find(x => x.Identifier == "Id1"), Is.Not.Null);
        }

        [Test]
        public void TestAddData()
        {
            int countBeforeAdd = Data.Length;

            dataManager.AddData(new PasswordData { Identifier = "NewId1" });
            db.SaveChanges();

            Assert.That(db.PASSWORDDATA.Count(), Is.EqualTo(++countBeforeAdd));
            Assert.That(db.PASSWORDDATA.FirstOrDefault(x => x.PWID == "NewId1"), Is.Not.Null);
        }

        [Test]
        public void TestDeleteData()
        {
            int countBeforeDelete = Data.Length;

            dataManager.Remove(new PasswordData { Identifier = "Id1" });

            Assert.That(db.PASSWORDDATA.Count(), Is.EqualTo(--countBeforeDelete));
            Assert.That(db.PASSWORDDATA.FirstOrDefault(x => x.PWID == "Id1"), Is.Null);

        }

        [Test]
        public async Task TestLoadDataAsync()
        {
            List<PasswordData> data = await dataManager.LoadDataAsync();

            Assert.That(data.Count, Is.EqualTo(9));
            Assert.That(data.Find(x => x.Identifier == "Id1"), Is.Not.Null);
        }

        [Test]
        public async Task TestAddDataAsync()
        {
            int countBeforeAdd = Data.Length;

            await dataManager.AddDataAsync(new PasswordData { Identifier = "NewId1" });
            await db.SaveChangesAsync();

            Assert.That(db.PASSWORDDATA.Count(), Is.EqualTo(++countBeforeAdd));
            Assert.That(db.PASSWORDDATA.FirstOrDefault(x => x.PWID == "NewId1"), Is.Not.Null);

        }

        [Test]
        public async Task TestDeleteDataAsync()
        {
            int countBeforeDelete = Data.Length;

            await dataManager.RemoveAsync(new PasswordData { Identifier = "Id1" });

            Assert.That(db.PASSWORDDATA.Count(), Is.EqualTo(--countBeforeDelete));
            Assert.That(db.PASSWORDDATA.FirstOrDefault(x => x.PWID == "Id1"), Is.Null);

        }

        private PASSWORDDATA[] Data => new[]
        {
            new PASSWORDDATA {PWID = "Id1", PWCOMMENT = "test1", PWDATA = "safePassword1"},
            new PASSWORDDATA {PWID = "Id3", PWCOMMENT = "test3", PWDATA = "safePassword3"},
            new PASSWORDDATA {PWID = "Id4", PWCOMMENT = "test4", PWDATA = "safePassword4"},
            new PASSWORDDATA {PWID = "Id5", PWCOMMENT = "test5", PWDATA = "safePassword5"},
            new PASSWORDDATA {PWID = "Id6", PWCOMMENT = "test6", PWDATA = "safePassword6"},
            new PASSWORDDATA {PWID = "Id7", PWCOMMENT = "test7", PWDATA = "safePassword7"},
            new PASSWORDDATA {PWID = "Id8", PWCOMMENT = "test8", PWDATA = "safePassword8"},
            new PASSWORDDATA {PWID = "Id2", PWCOMMENT = "test2", PWDATA = "safePassword2"},
            new PASSWORDDATA {PWID = "Id9", PWCOMMENT = "test9", PWDATA = "safePassword9"}
        };
    }
}