using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using NUnit.Framework;
using PasswordManagement.Backend.Security;

namespace PasswordManagement.Backend.Tests.Security
{
    public class PasswordTests
    {
        [TestCase("SafePassword", "CE60E1E2-8682-4CDF-87D0-E9701FEBDBDF")]
        [TestCase("PasswordSAFE", "5F86B0FE-0678-49D5-AE3C-95A84E5E0FDC")]
        public void PasswordHashTests(string passwordCamelCased, string salt)
        {
            var falseSalt = Password.GetSalt();

            string password = Password.GetHash(passwordCamelCased + salt);
            string otherPassword = Password.GetHash(passwordCamelCased.ToLower() + salt);

            string otherPassword2 = Password.GetHash(passwordCamelCased + falseSalt);

            Assert.That(password, Is.Not.EqualTo(otherPassword));
            Assert.That(password, Is.Not.EqualTo(otherPassword2));
            Assert.That(password, Is.EqualTo(Password.GetHash(passwordCamelCased + salt)));

        }
    }
}