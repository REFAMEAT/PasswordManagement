using System;
using System.Collections.Generic;
using NUnit.Framework;
using REFame.PasswordManagement.Backend.Login;
using REFame.PasswordManagement.Backend.Security;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.Model;

namespace REFame.PasswordManagement.Backend.Tests.Login
{
    [TestFixture(TestOf = typeof(LocalLogin))]
    public class LocalLoginTests
    {
        private LocalLogin localLogin;

        [SetUp]
        public void Setup()
        {
            System.IO.File.Create("testfile.bin").Dispose();
            BinaryHelper testHelper = new BinaryHelper("testfile.bin");

            USERDATA data = UserFactory.CreateUser("testuser", "testpassword");
            
            testHelper.Write(new BinaryData(data.USUSERNAME, data.USPASSWORD, data.USSALT)
            {
                Passwords = new List<PasswordData>()
                {
                    new PasswordData()
                    {
                        Comments = "TEST",
                        Password = Encryption.EncryptString("VERYIMPORTANT", "PWD"),
                        Description = "Description",
                        Identifier = "ID1"
                    }
                }
            });

            localLogin = new LocalLogin(testHelper);
        }

        [Test()]
        public void LocalLoginTest()
        {
            Assert.That(
                () => new LocalLogin(new BinaryHelper("testfile.bin")), 
                Throws.Nothing);
        }

        [Test()]
        public void DisposeTest()
        {
            localLogin.Dispose();

            Assert.That(() => localLogin.Validate("",""), 
                Throws.Exception.TypeOf(typeof(NullReferenceException)));
        }

        [Test()]
        public void ValidateTest()
        {
            string user = localLogin.Validate("testuser", "testpassword");

            Assert.That(user, Is.Not.Empty);
        }

        [Test()]
        public void NeedFirstUserTest()
        {
            System.IO.File.Create("testfile2.bin").Dispose();

            LocalLogin login = new LocalLogin(new BinaryHelper("testfile2.bin"));
            bool needUser = login.NeedFirstUser();

            Assert.That(needUser, Is.True);

        }

        [Test()]
        public void InitializeTest()
        {
            localLogin.Initialize();

            Assert.That(localLogin.InitSuccessful, Is.True);
        }
    }
}