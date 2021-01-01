using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using NUnit.Framework;
using REFame.PasswordManagement.AppCore;
using REFame.PasswordManagement.Data.DataManager;
using REFame.PasswordManagement.File;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.File.Binary.Factory;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Data.Tests.DataManager
{
    [TestFixture]
    public class FileDataManagerTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            PWCore.Create();
            PWCore.CurrentCore.RegisterType<IFolderProvider, FolderProvider>();
            PWCore.CurrentCore.RegisterType<IBinaryHelperFactory, BinaryHelperFactory>();
        }

        [SetUp]
        public void Setup()
        {
            var data = new BinaryData("", "", "")
            {
                Passwords = pwData.ToList()
            };

            using Stream s = new FileStream("data.bin", FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(s, data);
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete("data.bin");
        }

        [Test]
        public void TestConstructorValidPath()
        {
            Assert.That(new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin")), Is.TypeOf<FileDataManager>().And.Not.Null);
        }

        [Test]
        public void AddDataTest()
        {
            int countBeforeAdd = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            dataManager.AddData(new PasswordData { Identifier = "NewId1" });

            Assert.That(dataManager.LoadData().Count, Is.EqualTo(++countBeforeAdd));
        }

        [Test]
        public void DeleteDataTest()
        {
            int countBeforeDelete = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            bool deleted = dataManager.Remove(new PasswordData { Identifier = "Id1" });

            Assert.That(dataManager.LoadData().Count, Is.EqualTo(--countBeforeDelete));
            Assert.That(deleted, Is.True);
        }

        [Test]
        public void DeleteWrongFile()
        {
            int countBeforeDelete = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            bool deleted = dataManager.Remove(new PasswordData { Identifier = "WrongId" });

            Assert.That(deleted, Is.False);
            Assert.That(dataManager.LoadData().Count, Is.EqualTo(countBeforeDelete));
        }

        [Test]
        public async Task AddDataTestAsync()
        {
            int countBeforeAdd = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            await dataManager.AddDataAsync(new PasswordData { Identifier = "NewId1" });

            Assert.That((await dataManager.LoadDataAsync()).Count, Is.EqualTo(++countBeforeAdd));
        }

        [Test]
        public async Task DeleteDataTestAsync()
        {
            int countBeforeDelete = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            bool deleted = await dataManager.RemoveAsync(new PasswordData { Identifier = "Id1" });

            Assert.That((await dataManager.LoadDataAsync()).Count, Is.EqualTo(--countBeforeDelete));
            Assert.That(deleted, Is.True);
        }

        [Test]
        public async Task DeleteWrongFileAsync()
        {
            int countBeforeDelete = pwData.Length;
            var dataManager = new FileDataManager(new BinaryHelperFactory(new FolderProvider()).SetPath("data.bin"));

            bool deleted = await dataManager.RemoveAsync(new PasswordData { Identifier = "WrongId" });

            Assert.That(deleted, Is.False);
            Assert.That((await dataManager.LoadDataAsync()).Count, Is.EqualTo(countBeforeDelete));
        }

        private readonly PasswordData[] pwData =
        {
            new PasswordData
                {Comments = "Test1", Description = "Test1", Identifier = "Id1", Password = "superSafe1"},
            new PasswordData
                {Comments = "Test2", Description = "Test2", Identifier = "Id2", Password = "superSafe2"},
            new PasswordData
                {Comments = "Test3", Description = "Test3", Identifier = "Id3", Password = "superSafe3"},
            new PasswordData
                {Comments = "Test4", Description = "Test4", Identifier = "Id4", Password = "superSafe4"},
            new PasswordData
                {Comments = "Test5", Description = "Test5", Identifier = "Id5", Password = "superSafe5"},
            new PasswordData
                {Comments = "Test6", Description = "Test6", Identifier = "Id6", Password = "superSafe6"},
            new PasswordData
                {Comments = "Test7", Description = "Test7", Identifier = "Id7", Password = "superSafe7"},
            new PasswordData
                {Comments = "Test8", Description = "Test8", Identifier = "Id8", Password = "superSafe8"},
            new PasswordData
                {Comments = "Test9", Description = "Test9", Identifier = "Id9", Password = "superSafe9"},
            new PasswordData
                {Comments = "Test10", Description = "Test10", Identifier = "Id10", Password = "superSafe10"},
            new PasswordData
                {Comments = "Test11", Description = "Test11", Identifier = "Id11", Password = "superSafe11"}
        };
    }
}