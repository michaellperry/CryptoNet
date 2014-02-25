using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace EncryptionTests
{
    [TestClass]
    public class SymmetricTests
    {
        [TestMethod]
        public void GenerateARandomAESKey()
        {
            SymmetricAlgorithm aes = null;
            aes.KeySize = 256;

            aes.Key.Length.Should().Be(32);     // 256 / 8
            aes.IV.Length.Should().Be(16);      // 128 / 8
        }

        [TestMethod]
        public void EncryptAMessageWithAES()
        {
            string message = "Alice knows Bob's secret.";
            SymmetricAlgorithm aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;

            byte[] key = aes.Key;
            byte[] iv = aes.IV;

            byte[] encryptedMessage = EncryptWithAes(message, aes);

            string decryptedMessage = DecryptMessageWithAes(
                key, iv, encryptedMessage);

            decryptedMessage.Should().Be(message);
        }

        [TestMethod]
        public void UsingTheSameInitializationVector()
        {
            string message1 = "Alice knows Bob's secret.";
            string message2 = "Alice knows Bob's favorite color.";
            SymmetricAlgorithm aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;

            byte[] encryptedMessage1 = EncryptWithAes(message1, aes);
            byte[] encryptedMessage2 = EncryptWithAes(message2, aes);

            Enumerable.SequenceEqual(
                encryptedMessage1.Take(16),
                encryptedMessage2.Take(16))
                .Should().BeTrue();
        }

        [TestMethod]
        public void UsingADifferentInitializationVector()
        {
            string message1 = "Alice knows Bob's secret.";
            string message2 = "Alice knows Bob's favorite color.";
            SymmetricAlgorithm aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;

            byte[] key = aes.Key;
            byte[] encryptedMessage1 = EncryptWithAes(message1, aes);
            aes = new AesCryptoServiceProvider();
            aes.Key = key;
            byte[] encryptedMessage2 = EncryptWithAes(message2, aes);

            Enumerable.SequenceEqual(
                encryptedMessage1.Take(16),
                encryptedMessage2.Take(16))
                .Should().BeFalse();
        }

        private static byte[] EncryptWithAes(
            string message, SymmetricAlgorithm aes)
        {
            MemoryStream memoryStream = new MemoryStream();

            return memoryStream.ToArray();
        }

        private static string DecryptMessageWithAes(
            byte[] key, byte[] iv, byte[] encryptedMessage)
        {
            SymmetricAlgorithm provider = new AesCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(encryptedMessage);

            return string.Empty;
        }
    }
}
