using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Chronicles.Web.Utility
{
    public class GravatarProvider
    {
        private const string GravatarUrlFormat = @"http://www.gravatar.com/avatar.php?gravatar_id={0}&rating=PG&size=50&default=identicon";

        public static string GetIcon(string emailId)
        {
            string properEmail = emailId.Trim().ToLower();

            string hash = ComputeMd5Hash(properEmail);

            return string.Format(GravatarUrlFormat, hash);
        }

        private static string ComputeMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();  // Return the hexadecimal string.  
        }
    }
}
