using NUnit.Framework;
using PasswordManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace PasswordManagement.Model.Tests
{
    [TestFixture()]
    public class AppCoreTests
    {
        [Test()]
        public void StartCoreTest()
        {
            AppCore.StartCore();

            Assert.That(AppCore.StartCore, Throws.TypeOf(typeof(ApplicationException)));
        }
    }
}