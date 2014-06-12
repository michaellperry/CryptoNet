using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.IO;
using FluentAssertions;

namespace EncryptionTests
{
    [TestClass]
    public class HashingTests
    {
        [TestMethod]
        public void HashFunctionIsDeterministic()
        {
            string password = "pass@word1";
            byte[] hash1 = HashPassword(password);
            byte[] hash2 = HashPassword(password);
            
            Convert.ToBase64String(hash2).Should().Be(
                Convert.ToBase64String(hash1));
        }

        private static byte[] HashPassword(string password)
        {
            var memory = new MemoryStream();
            using (var writer = new StreamWriter(memory))
            {
                writer.Write(password);
            }
            byte[] bytes = memory.ToArray();

            throw new NotImplementedException();
        }
    }
}
