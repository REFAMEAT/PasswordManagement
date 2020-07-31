using NUnit.Framework;
using PasswordManagement.Backend.Security;
using PasswordManagement.Database.Model;
using System.Diagnostics;

namespace PasswordManagement.Backend.Tests.Security
{
    public class UserFactoryTests
    {
        [TestCase(5), TestCase(4)]
        [TestCase(3), TestCase(2)]
        public void UserFactoryDelayTest(int executeTimes)
        {
            int minimumTimeNeeded = executeTimes * 500;
            Stopwatch sw = new Stopwatch();

            sw.Start();
            for (int i = 0; i < executeTimes; i++)
            {
                UserFactory.CreateUser("test", "test");
            }

            sw.Stop();

            Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(minimumTimeNeeded));
        }

        [Test]
        public void UserFactoryCreateTest()
        {
            string userName = "Max Mustermann";
            string password = "MusterPassword";

            USERDATA userData = UserFactory.CreateUser(userName, password);

            string cryptUserName = Password.GetHash(userName + userData.USSALT);
            string cryptPassword = Password.GetHash(password + userData.USSALT);


            Assert.That(userData.USPASSWORD, Is.Not.EqualTo(password));
            Assert.That(userData.USUSERNAME, Is.Not.EqualTo(userName));

            Assert.That(userData.USUSERNAME, Is.EqualTo(cryptUserName));
            Assert.That(userData.USPASSWORD, Is.EqualTo(cryptPassword));
        }
    }
}