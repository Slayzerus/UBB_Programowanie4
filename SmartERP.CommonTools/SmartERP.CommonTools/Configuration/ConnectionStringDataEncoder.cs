namespace SmartERP.CommonTools.Configuration
{
    using System;

    public static class ConnectionStringDataEncoder
    {
        private const string SaltKey = "W!s$r=P6P#Sj@";

        public static string DecryptHash(string hash)
        {
            byte[] data = Convert.FromBase64String(hash);
            using (System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(SaltKey));
                using (System.Security.Cryptography.TripleDESCryptoServiceProvider tripDes =
                    new System.Security.Cryptography.TripleDESCryptoServiceProvider
                    {
                        Key = keys,
                        Mode = System.Security.Cryptography.CipherMode.ECB,
                        Padding = System.Security.Cryptography.PaddingMode.PKCS7
                    })
                {
                    System.Security.Cryptography.ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return System.Text.Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}