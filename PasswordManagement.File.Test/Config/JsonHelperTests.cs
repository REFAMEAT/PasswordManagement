using System.IO;
using NUnit.Framework;

namespace PasswordManagement.File.Config.Tests
{
    [TestFixture]
    public class JsonHelperTests
    {
        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete(JsonHelper<TestModel>.GetPath());
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
    }

    public class TestModel
    {
        public string StringValue { get; set; }
        public int IntValue { get; set; }
        public char CharValue { get; set; }
    }
}