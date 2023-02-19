using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystemAPI.Tools
{
    public class CryptographyManager
    {

        public static string GetSHA256(string value)
        {
            StringBuilder builder = new StringBuilder();

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(value);
                byte[] hash = sha256.ComputeHash(bytes);

                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("X2"));
                }
            }

            return builder.ToString();
        }


        public static string GetMd5(string value)
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(value))
            {

                using (MD5 md5 = MD5.Create())
                {
                    byte[] bytes = Encoding.Unicode.GetBytes(value);
                    byte[] hash = md5.ComputeHash(bytes);

                    foreach (byte b in hash)
                    {
                        builder.Append(b.ToString("X2"));
                    }
                }
            }

            return builder.ToString();
        }
    }
}
