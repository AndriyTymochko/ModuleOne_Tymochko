using System;
using System.Text;
using System.Security.Cryptography;

namespace Security
{
    public static class SecurityHelper
    {
        #region Fields
        private const string _defaultKey = "!MainAcademyLab*";
        #endregion

        #region Methods

        #region Protection
        public static string ProtectConfigParameter(string value)
        {
            return ProtectConfigParameter(value, _defaultKey);
        }

        public static string ProtectConfigParameter(string value, string key)
        {
            try
            {
                string s = key;
                MD5 md5 = (MD5)new MD5CryptoServiceProvider();
                TripleDES tripleDes = (TripleDES)new TripleDESCryptoServiceProvider();
                tripleDes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(s));
                tripleDes.IV = new byte[tripleDes.BlockSize / 8];
                ICryptoTransform encryptor = tripleDes.CreateEncryptor();
                byte[] bytes = Encoding.Unicode.GetBytes(value);
                return Convert.ToBase64String(encryptor.TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region Unprotection
        public static string UnprotectConfigParameter(string encr_value)
        {
            return UnprotectConfigParameter(encr_value, _defaultKey);
        }

        public static string UnprotectConfigParameter(string encr_value, string key)
        {
            try
            {
                string s = key;
                MD5 md5 = (MD5)new MD5CryptoServiceProvider();
                TripleDES tripleDes = (TripleDES)new TripleDESCryptoServiceProvider();
                tripleDes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(s));
                tripleDes.IV = new byte[tripleDes.BlockSize / 8];
                byte[] inputBuffer = Convert.FromBase64String(encr_value);
                return Encoding.Unicode.GetString(tripleDes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #endregion
    }
}
