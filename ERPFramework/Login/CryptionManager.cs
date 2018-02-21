using System;
using System.IO;
using System.Security.Cryptography;

namespace ERPFramework.Data
{
    /// <summary>
    /// Summary description for Cryption.
    /// </summary>
    public static class Cryption
    {
        private static RijndaelManaged Algorithm;
        private static MemoryStream memStream;
        private static ICryptoTransform EncryptorDecryptor;
        private static CryptoStream crStream;
        private static StreamWriter strWriter;
        private static StreamReader strReader;

        private static byte[] key = {
								 0x96,0x41,0x8b,0x6e,0x22,0x45,0x16,0x4b,0x95,0xae,0xe8,0x2a,0x9d,0x48,0xd2,0x18,
								 0xec,0xcb,0x71,0xc,0x9a,0xa8,0x24,0x42,0x93,0x55,0x0,0xfb,0x18,0xa8,0x6a,0x27
							 };

        private static byte[] iv = {
								0xa2,0xca,0x71,0x2e,0xcb,0x4c,0xe4,0x41,0x8b,0x8e,0x1d,0x5,0xb0,0x27,0x2f,0xd,
								0x7b,0xc6,0xe9,0x63,0xd4,0x90,0x90,0x4c,0x8c,0x8,0x4,0xeb,0x4f,0xe7,0x58,0xbb
							};

        private static string pwd_str;
        private static byte[] pwd_byte;

        public static string Encrypt(string s)
        {
            Algorithm = new RijndaelManaged();

            Algorithm.BlockSize = 256;
            Algorithm.KeySize = 256;

            memStream = new MemoryStream();

            EncryptorDecryptor = Algorithm.CreateEncryptor(key, iv);

            crStream = new CryptoStream(memStream, EncryptorDecryptor, CryptoStreamMode.Write);

            strWriter = new StreamWriter(crStream);

            strWriter.Write(s);

            strWriter.Flush();
            crStream.FlushFinalBlock();

            pwd_byte = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(pwd_byte, 0, (int)pwd_byte.Length);

            pwd_str = Convert.ToBase64String(pwd_byte);

            // pwd_str = new UnicodeEncoding().GetString(pwd_byte);

            return pwd_str;
        }

        public static string Decrypt(string s)
        {
            Algorithm = new RijndaelManaged();

            Algorithm.BlockSize = 256;
            Algorithm.KeySize = 256;

            MemoryStream memStream = new MemoryStream(Convert.FromBase64String(s));

            ICryptoTransform EncryptorDecryptor = Algorithm.CreateDecryptor(key, iv);
            memStream.Position = 0;
            CryptoStream crStream = new CryptoStream(memStream, EncryptorDecryptor, CryptoStreamMode.Read);
            strReader = new StreamReader(crStream);

            return strReader.ReadToEnd();
        }
    }
}