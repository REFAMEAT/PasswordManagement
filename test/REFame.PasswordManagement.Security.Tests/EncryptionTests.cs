using System;
using NUnit.Framework;

namespace REFame.PasswordManagement.Security.Tests
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