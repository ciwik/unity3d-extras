using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Extras.Cryptography
{
    public static partial class Cipher
    { 
        public static class AES
        {
            private static int KeyLength = 128;
            private const string SALT_KEY = "ShMG8hLyZ7k~Ge5@";
            private const string VI_KEY = "~6YUi0Sv5@|{aOZO";
    
            public static byte[] Encrypt(byte[] value, string key)
            {
                var keyBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(SALT_KEY)).GetBytes(KeyLength / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.UTF8.GetBytes(VI_KEY));
    
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(value, 0, value.Length);
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                        memoryStream.Close();
    
                        return memoryStream.ToArray();
                    }
                }
            }
    
            public static byte[] Encrypt(string value, string key) => Encrypt(Encoding.UTF8.GetBytes(value), key);

            public static string Decrypt(byte[] value, string key)
            {
                var keyBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(SALT_KEY)).GetBytes(KeyLength / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.None };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.UTF8.GetBytes(VI_KEY));
    
                using (var memoryStream = new MemoryStream(value))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        var plainTextBytes = new byte[value.Length];
                        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
    
                        memoryStream.Close();
                        cryptoStream.Close();
    
                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
                    }
                }
            }

            public static string Decrypt(string value, string key) => Decrypt(Convert.FromBase64String(value), key);
        }
    }
}
