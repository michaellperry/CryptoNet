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

        [TestMethod]
        public void PasswordBasedKeyDerivationFunctionIsSlower()
        {
            string password = "pass@word1";
            byte[] hash1 = PBKDF(password);
            byte[] hash2 = PBKDF(password);

            Convert.ToBase64String(hash1).Should().Be(
                Convert.ToBase64String(hash2));
        }

        private static byte[] PBKDF(string password)
        {
            var hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] stage = HashPassword(password);
            byte[] hash = new byte[stage.Length];
            Array.Copy(stage, hash, stage.Length);

            for (int round = 0; round < 100000; ++round)
            {
                stage = hashAlgorithm.ComputeHash(stage);
                XorBytes(hash, stage);
            }
            return hash;
        }

        private static void XorBytes(byte[] target, byte[] source)
        {
            for (int index = 0; index < target.Length; index++)
            {
                target[index] ^= source[index];
            }
        }
    }
}
