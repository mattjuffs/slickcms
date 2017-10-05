using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SlickCMS.Core
{
    public class RegEx
    {
        /// <summary>
        /// Checks if a string is a valid email address
        /// Thanks to http://msdn.microsoft.com/en-us/library/01escwtf.aspx
        /// </summary>
        /// <param name="email">string to validate</param>
        /// <returns>true/false depending on whether or not string is a valid email</returns>
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        }
    }
}
