using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using PasswordManagement.File.Binary;
using PasswordManagement.Model;

namespace PasswordManagement.File.Test.Binary
{
    [TestFixture]
    public class BinaryHelperTests
    {
        [SetUp]
        public void Setup()
        {
            BinaryData data = new BinaryData("", "", "")
            {
                Passwords = new List<PasswordData>
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
                }
            };

            using Stream s = new FileStream("data.bin", FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(s, data);
        }

        [Test]
        public void TestReadDataFromFile()
        {
            BinaryHelper helper = new BinaryHelper("data.bin");

            BinaryData data = helper.GetData();

            Assert.That(data.Passwords.Count, Is.EqualTo(11));
            Assert.That(data.Passwords.Find(x => x.Identifier == "Id1"), Is.Not.Null);
            Assert.That(data.UserNameHash, Is.Empty);
        }

        [Test]
        public void TestWriteDataToFile()
        {
            BinaryHelper helper = new BinaryHelper("data.bin");
            PasswordData dataToAdd = new PasswordData() { Comments = "NewComment", Description = "NewDescription", Password = "NewPassword", Identifier = "NewID" };
            BinaryData data = helper.GetData();
            data.Passwords.Add(dataToAdd);

            helper.Write(data);

            Assert.That(helper.GetData().Passwords.Count, Is.EqualTo(12));
            Assert.That(helper.GetData().Passwords.Find(x => x.Identifier == "NewID"), Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete("data.bin");
        }
    }
}