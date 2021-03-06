﻿using System.Diagnostics;
using NUnit.Framework;
using REFame.PasswordManagement.DB.Entities;
using REFame.PasswordManagement.Login.Contracts;

namespace REFame.PasswordManagement.Security.Tests
{
    public class UserFactoryTests
    {
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        public void UserFactoryDelayTest(int executeTimes)
        {
            int minimumTimeNeeded = executeTimes * 500;
            var sw = new Stopwatch();

            sw.Start();
            for (var i = 0; i < executeTimes; i++)
            {
                UserFactory.CreateUser(new User(){UserName = "username", FullName = "User Name"},"test");
            }

            sw.Stop();

            Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(minimumTimeNeeded));
        }

        [Test]
        public void UserFactoryCreateTest()
        {
            var userName = "Max.Mustermann";
            var fullName = "Max Mustermann";
            var password = "MusterPassword";

            USERDATA userData = UserFactory.CreateUser(new User(){ UserName =  userName, FullName = fullName}, password);

            string cryptUserName = Encryption.EncryptString(userName);
            string cryptPassword = Password.GetHash(password + userData.USSALT);


            Assert.That(userData.USPASSWORD, Is.Not.EqualTo(password));
            Assert.That(userData.USUSERNAME, Is.Not.EqualTo(userName));

            Assert.That(userData.USUSERNAME, Is.EqualTo(cryptUserName));
            Assert.That(userData.USPASSWORD, Is.EqualTo(cryptPassword));
        }
    }
}