using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionApp {
    //https://tomrucki.com/posts/aes-encryption-in-csharp/
    public class Encryption {
        private const int AesBlockByteSize = 128 / 8;
        private static readonly RandomNumberGenerator Random = RandomNumberGenerator.Create();
        public string Encrypt(string input, string password) {
            byte[] key = GetKey(password);
            using(Aes aes = Aes.Create()) {
                byte[] iv = GenerateRandomBytes(AesBlockByteSize);
                using(ICryptoTransform encryptor = aes.CreateEncryptor(key, iv)) {
                    byte[] plainText = Encoding.Unicode.GetBytes(input);
                    byte[] cipherText = encryptor.TransformFinalBlock(plainText, 0, plainText.Length);
                    byte[] result = new byte[iv.Length + cipherText.Length];
                    iv.CopyTo(result, 0);
                    cipherText.CopyTo(result, iv.Length);

                    return Encoding.Unicode.GetString(result);
                }
            }
        }

        public string Decrypt(string input, string password) {
            try {
                byte[] key = GetKey(password);
                using (Aes aes = Aes.Create()) {
                    byte[] iv = Encoding.Unicode.GetBytes(input).Take(AesBlockByteSize).ToArray();
                    byte[] cipherText = Encoding.Unicode.GetBytes(input).Skip(AesBlockByteSize).ToArray();

                    using (ICryptoTransform encryptor = aes.CreateDecryptor(key, iv)) {
                        byte[] decryptedBytes = encryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                        return Encoding.Unicode.GetString(decryptedBytes);
                    }
                }
            } catch (CryptographicException) {
                return "Can't decrypt.";
            }
        }

        private static byte[] GetKey(string password) {
            byte[] keyBytes = Encoding.Unicode.GetBytes(password);
            using(MD5 md5 = MD5.Create()) {
                return md5.ComputeHash(keyBytes);
            }
        }

        private static byte[] GenerateRandomBytes(int numberOfBytes) {
            byte[] randomBytes = new byte[numberOfBytes];
            Random.GetBytes(randomBytes);
            return randomBytes;
        }
    }
}
