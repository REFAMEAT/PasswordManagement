using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using NUnit.Framework;
using REFame.PasswordManagement.File.Config;

namespace REFame.PasswordManagement.File.Tests.Config
{
    [TestFixture]
    public class JsonHelperTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            JsonPathInfo.InTest = true;
        }

        [SetUp]
        public void Setup()
        {
            DirectoryInfo directory = new FileInfo(JsonHelper<TestModel>.GetPath()).Directory;

            if (!directory?.Exists == true)
            {
                directory.Create();
            }
        }
        
        [TearDown]
        public void TearDown()
        {
            try
            {
                FileInfo file = new FileInfo(JsonHelper<TestModel>.GetPath());
                FileSystem.DeleteDirectory(file.DirectoryName, DeleteDirectoryOption.DeleteAllContents);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [Test]
        public void GetDefaultDataTest()
        {
            TestModel data = JsonHelper<TestModel>.GetData(new TestModel());
            Assert.That(data, Is.Not.Null);
        }

        [Test]
        public void GetSavedDataTest()
        {
            JsonHelper<TestModel>.GetData(new TestModel());

            JsonHelper<TestModel>.WriteData(new TestModel
            {
                CharValue = 'x',
                IntValue = 5,
                StringValue = "TEST"
            });

            TestModel model = JsonHelper<TestModel>.GetData(new TestModel());

            Assert.That(model, Is.Not.EqualTo(new TestModel()));
            Assert.That(model.IntValue, Is.EqualTo(5));
            Assert.That(model.CharValue, Is.EqualTo('x'));
            Assert.That(model.StringValue, Is.EqualTo("TEST"));
        }

        [Test]
        public void GetThrows()
        {
            Assert.That(() => JsonHelper<TestModel>.GetData(), Throws.TypeOf(typeof(FileNotFoundException)));
        }

        [Test]
        public void WriteDataTest()
        {
            JsonHelper<TestModel>.WriteData(new TestModel
            {
                CharValue = 'y',
                IntValue = 44,
                StringValue = "HalloTest"
            });

            TestModel model = JsonHelper<TestModel>.GetData();

            Assert.That(model.IntValue, Is.EqualTo(44));
            Assert.That(model.CharValue, Is.EqualTo('y'));
            Assert.That(model.StringValue, Is.EqualTo("HalloTest"));
        }

        [Test]
        public void GetPathIsValid()
        {
            string path = JsonHelper<TestModel>.GetPath();

            if (path.Contains("_dev"))
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail("Path is invalid");
            }
        }
    }

    public class TestModel
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public char CharValue { get; set; }
    }
}