using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SlickCMS.Core.Enums;

namespace SlickCMS.Core
{
    public class Hash
    {
        /// <summary>
        /// Generates a one-way Hash of a string
        /// </summary>
        /// <param name="Data">string to Hash</param>
        /// <param name="Type">Type of Hash to perform on the string</param>
        /// <returns>Hash</returns>
        public static string GenerateHash(string data, HashType hashType)
        {
            // example use: Hash.GenerateHash("Hello World", Hash.HashType.MD5);

            // references:
            // http://msdn.microsoft.com/en-us/library/system.security.cryptography.sha1.aspx
            // http://www.stardeveloper.com/articles/display.html?article=2003062001&page=1
            // http://dotnetpulse.blogspot.com/2007/12/sha1-hash-calculation-in-c.html

            byte[] hashData = null;
            byte[] result = null;

            hashData = Encoding.ASCII.GetBytes(data);

            // TODO: add additional hashes if available:
            switch (hashType)
            {
                case HashType.SHA1:
                    var sha1_ = new SHA1CryptoServiceProvider();
                    result = sha1_.ComputeHash(hashData);
                    break;
                case HashType.SHA256:
                    var sha256 = new SHA256CryptoServiceProvider();
                    result = sha256.ComputeHash(hashData);
                    break;
                case HashType.SHA512:
                    var sha512 = new SHA512CryptoServiceProvider();
                    result = sha512.ComputeHash(hashData);
                    break;
                case HashType.MD5:
                    var md5 = new MD5CryptoServiceProvider();
                    result = md5.ComputeHash(hashData);
                    break;
                default:
                    return "";
            }

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
