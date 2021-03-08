using NUnit.Framework;
using System;

namespace REFame.PasswordManagement.AppCore.Tests
{
    [TestFixture()]
    public class PWCoreTests
    {
        [Test()]
        public void CreateTest()
        {
            PWCore.Create();

            Assert.That(PWCore.CurrentCore, Is.Not.Null);
        }

        [Test]
        public void CreateThrowsApplicationException()
        {
            PWCore.Create();

            Assert.That(PWCore.Create, 
                Throws.Exception.TypeOf(typeof(ApplicationException)));
        }

        [TearDown]
        public void TearDown()
        {
            PWCore.CurrentCore = null;
        }
    }
}