using System;
using NUnit.Framework;

namespace REFame.PasswordManagement.Model.Tests
{
    [TestFixture]
    public class AppCoreTests
    {
        [Test]
        public void StartCoreTest()
        {
            AppCore.StartCore();

            Assert.That(AppCore.StartCore, Throws.TypeOf(typeof(ApplicationException)));
        }
    }
}