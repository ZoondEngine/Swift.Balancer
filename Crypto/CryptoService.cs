using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Swift.Balancer.App;
using Swift.Balancer.Database;
using Swift.Balancer.Database.Models;

namespace Swift.Balancer.Crypto
{
    public class CryptoService : ApplicationService
    {
        public override string Name()
            => "[BALANCER] - Crypto Service";

        public override Version Version()
            => System.Version.Parse("1.0.0.0");

        public bool Challenge(string encrypted)
        {
            var service = FindObjectOfType<DatabaseService>();
            using var db = service.GetContext();
            var keys = db.ChallengeKeys.ToList();
            var result = false;

            foreach (var key in from key in keys let word = key.Word where word == Decrypt(encrypted, key.Key) select key)
            {
                result = true;
            }

            return result;
        }
        
        public string Encrypt(string text, string keyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using var aesAlg = Aes.Create();
            using var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }

            var iv = aesAlg.IV;
            var decryptedContent = msEncrypt.ToArray();
            var result = new byte[iv.Length + decryptedContent.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipherText, string keyString)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            var iv = new byte[16];
            var cipher = new byte[16];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            
            var key = Encoding.UTF8.GetBytes(keyString);
            
            using var aesAlg = Aes.Create();
            using var decryptor = aesAlg.CreateDecryptor(key, iv);
            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            
            return srDecrypt.ReadToEnd();
        }
    }
}