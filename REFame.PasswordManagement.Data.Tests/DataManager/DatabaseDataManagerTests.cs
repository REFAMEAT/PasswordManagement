using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using REFame.PasswordManagement.Data.DataManager;
using REFame.PasswordManagement.Database.DbSet;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.Database.Tests;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Data.Tests.DataManager
{
    [TestFixture]
    public class DatabaseDataManagerTests : DatabaseSetup
    {
        [SetUp]
        public async Task Setup()
        {
            dataSet = new DataSet<PASSWORDDATA>(options);
            dataManager = new DatabaseDataManager(dataSet);

            await dataSet.Entities.AddRangeAsync(Data);

            await dataSet.SaveChangesAsync();
        }

        [TearDown]
        public void TearDown()
        {
            dataSet.RemoveRange(dataSet.Entities);
            dataSet.SaveChanges();
        }

        private DataSet<PASSWORDDATA> dataSet;
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

            dataManager.AddData(new PasswordData {Identifier = "NewId1"});
            dataSet.SaveChanges();

            Assert.That(dataSet.Entities.Count(), Is.EqualTo(++countBeforeAdd));
            Assert.That(dataSet.Find<PASSWORDDATA>("NewId1"), Is.Not.Null);
        }

        [Test]
        public void TestDeleteData()
        {
            int countBeforeDelete = Data.Length;

            dataManager.Remove(new PasswordData {Identifier = "Id1"});

            Assert.That(dataSet.Entities.Count(), Is.EqualTo(--countBeforeDelete));
            Assert.That(dataSet.Find<PASSWORDDATA>("Id1"), Is.Null);
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
            await dataSet.SaveChangesAsync();

            Assert.That(dataSet.Entities.Count(), Is.EqualTo(++countBeforeAdd));
            Assert.That(dataSet.Find<PASSWORDDATA>("NewId1"), Is.Not.Null);
        }

        [Test]
        public async Task TestDeleteDataAsync()
        {
            int countBeforeDelete = Data.Length;

            await dataManager.RemoveAsync(new PasswordData { Identifier = "Id1" });

            Assert.That(dataSet.Entities.Count(), Is.EqualTo(--countBeforeDelete));
            Assert.That(dataSet.Find<PASSWORDDATA>("Id1"), Is.Null);
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