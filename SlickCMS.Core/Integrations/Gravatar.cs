using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SlickCMS.Core.Integrations
{
    // http://en.gravatar.com/
    public class Gravatar
    {
        public static string GetUrl(string email, int size = 80)
        {
            // https://www.gravatar.com/avatar/HASH

            // 1. Trim leading and trailing whitespace from an email address
            // 2. Force all characters to lower-case
            string data = email.Trim().ToLower();

            // 3. md5 hash the final string
            byte[] hashData = Encoding.ASCII.GetBytes(data);
            var md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(hashData);
            string hash = BitConverter.ToString(result).Replace("-", "").ToLower();

            // specifying the optional Size parameter
            return string.Format("https://www.gravatar.com/avatar/{0}.jpg?s={1}", hash, size);
        }
    }
}
