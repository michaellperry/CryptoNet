using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using FluentAssertions;
using System.Linq;
using System.IO;

namespace EncryptionTests
{
    [TestClass]
    public class AsymmetricTests
    {
        [TestMethod]
        public void GenerateAnRsaKeyPair()
        {
            AsymmetricAlgorithm rsa = null;

            string encodedKey = rsa.ToXmlString(true);

            encodedKey.Should().Contain("<Modulus>");
            encodedKey.Should().Contain("<Exponent>AQAB</Exponent>");
            encodedKey.Should().Contain("<D>");
        }

        [TestMethod]
        public void ExponentIsAlways65537()
        {
            byte[] exponent = null;

            exponent.Length.Should().Be(3);
            long number =
                ((long)exponent[2] << 16) +
                ((long)exponent[1] << 8) +
                ((long)exponent[0]);
            number.Should().Be(65537);
        }

        [TestMethod]
        public void EncryptASymmetricKey()
        {
            var rsa = new RSACryptoServiceProvider(2048);

            byte[] blob = rsa.ExportCspBlob(false);
            var publicKey = new RSACryptoServiceProvider();
            publicKey.ImportCspBlob(blob);

            var aes = new AesCryptoServiceProvider();
            aes.KeySize = 256;
            byte[] encryptedKey = null;
            byte[] decryptedKey = null;

            Enumerable.SequenceEqual(
                decryptedKey,
                aes.Key)
                .Should().BeTrue();
        }

        [TestMethod]
        public void SignAMessage()
        {
            var rsa = new RSACryptoServiceProvider(2048);

            byte[] blob = rsa.ExportCspBlob(false);
            var publicKey = new RSACryptoServiceProvider();
            publicKey.ImportCspBlob(blob);

            string message = "Alice knows Bob's secret.";
            var memory = new MemoryStream();
            using (var writer = new StreamWriter(memory))
            {
                writer.Write(message);
            }

            var hashFunction = new SHA256CryptoServiceProvider();
            byte[] signature = null;

            bool verified = false;
            verified.Should().BeTrue();
        }
    }
}
