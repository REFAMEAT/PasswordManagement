using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using PasswordManagement.Backend.Data;
using PasswordManagement.File.Binary;
using PasswordManagement.Model;

namespace PasswordManagement.Backend.Tests.Data
{
    [TestFixture]
    public class FileDataManagerTests
    {
        [SetUp]
        public void Setup()
        {
            BinaryData data = new BinaryData("", "", "")
            {
                Passwords = pwData.ToList()
            };

            using Stream s = new FileStream("data.bin", System.IO.FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(s, data);
        }

        [Test]
        public void TestConstructorInvalidPath()
        {
            Assert.Throws<FileNotFoundException>(() => new FileDataManager("foo.foo"));
        }

        [Test]
        public void TestConstructorValidPath()
        {
            Assert.That(new FileDataManager("data.bin"), Is.TypeOf<FileDataManager>().And.Not.Null);
        }

        [Test]
        public void AddDataTest()
        {
            int countBeforeAdd = pwData.Length;
            FileDataManager dataManager = new FileDataManager("data.bin");

            dataManager.AddData(new PasswordData() {Identifier = "NewId1"});

            Assert.That(dataManager.LoadData().Count, Is.EqualTo(++countBeforeAdd));
        }

        [Test]
        public void DeleteDataTest()
        {
            int countBeforeDelete = pwData.Length;
            FileDataManager dataManager = new FileDataManager("data.bin");

            bool deleted = dataManager.Remove(new PasswordData() {Identifier = "Id1"});

            Assert.That(dataManager.LoadData().Count, Is.EqualTo(--countBeforeDelete));
            Assert.That(deleted, Is.True);
        }

        [Test]
        public void DeleteWrongFile()
        {
            int countBeforeDelete = pwData.Length;
            FileDataManager dataManager = new FileDataManager("data.bin");

            bool deleted = dataManager.Remove(new PasswordData() {Identifier = "WrongId"});

            Assert.That(deleted, Is.False);
            Assert.That(dataManager.LoadData().Count, Is.EqualTo(countBeforeDelete));
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete("data.bin");
        }

        private PasswordData[] pwData = new PasswordData[]
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
                {Comments = "Test11", Description = "Test11", Identifier = "Id11", Password = "superSafe11"},
        };
    }
}