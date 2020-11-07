using System;
using NUnit.Framework;
using REFame.PasswordManagement.Backend.Security;

namespace REFame.PasswordManagement.Backend.Tests.Security
{
    public class EncryptionTests
    {
        [Test]
        public void EnDecryptString()
        {
            var key = Guid.NewGuid().ToString();
            var text = Guid.NewGuid().ToString();

            string cryptText = Encryption.EncryptString(text, key);

            Assert.That(text, Is.EqualTo(Encryption.DecryptString(cryptText, key)));
        }
    }
}