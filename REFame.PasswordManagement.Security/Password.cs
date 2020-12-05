using System;
using System.Security.Cryptography;
using System.Text;

namespace REFame.PasswordManagement.Security
{
    /// <summary>
    ///     Class for Hashing and Salting Passwords
    /// </summary>
    public class Password
    {
        public static string GetSalt()
        {
            var bytes = new byte[128 / 8];
            using var keyGenerator = RandomNumberGenerator.Create();
            keyGenerator.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static string GetHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using var sha256 = SHA256.Create();

            // Send a sample text to hash.  
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get the hashed string.  
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}