using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using REFame.PasswordManagement.Database.Model;
using REFame.PasswordManagement.File;
using REFame.PasswordManagement.File.Binary;
using REFame.PasswordManagement.File.Binary.Factory;
using REFame.PasswordManagement.File.Contracts.Binary;
using REFame.PasswordManagement.Model;
using REFame.PasswordManagement.Security;

namespace REFame.PasswordManagement.Login.Tests
{
    [TestFixture(TestOf = typeof(LocalLogin))]
    public class LocalLoginTests
    {
        private LocalLogin localLogin;

        [SetUp]
        public void Setup()
        {
            System.IO.File.Create("testfile.bin").Dispose();
            IBinaryHelper testHelper = new BinaryHelper();
            testHelper.OverwriteDefaultPath("testfile.bin");

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

            IBinaryHelperFactory helperFactory = new BinaryHelperFactory(new TestFolderProvider());
            helperFactory.SetPath("testfile.bin");

            localLogin = new LocalLogin(helperFactory);
        }

        [Test()]
        public void LocalLoginTest()
        {
            Assert.That(
                () =>
                {
                    IBinaryHelperFactory helperFactory = new BinaryHelperFactory(new TestFolderProvider());
                    helperFactory.SetPath("testfile.bin");
                    return new LocalLogin(helperFactory);
                }, 
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
            System.IO.File.Delete("testfile2.bin");
            System.IO.File.Create("testfile2.bin").Dispose();

            IBinaryHelperFactory binaryHelperFactory = new BinaryHelperFactory(new TestFolderProvider()).SetPath("testfile2.bin");
            LocalLogin login = new LocalLogin(binaryHelperFactory);
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

    class TestFolderProvider : IFolderProvider
    {
        public TestFolderProvider()
        {
            AppDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                "PasswordManagementTest");
            //if (!Directory.Exists(AppDataFolder))
            //{
            //    Directory.CreateDirectory(AppDataFolder);
            //}
        }
        public string AppDataFolder { get; set; }
    }
}