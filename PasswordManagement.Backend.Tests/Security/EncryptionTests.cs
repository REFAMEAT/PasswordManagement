using NUnit.Framework;
using PasswordManagement.Backend.Security;
using System;

namespace PasswordManagement.Backend.Tests.Security
{
    public class EncryptionTests
    {
        [Test]
        public void EnDecryptString()
        {
            string key = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();

            string cryptText = Encryption.EncryptString(text, key);

            Assert.That(text, Is.EqualTo(Encryption.DecryptString(cryptText, key)));
        }
    }
}