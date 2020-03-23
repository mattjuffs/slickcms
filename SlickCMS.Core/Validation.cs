using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace SlickCMS.Core
{
    public class Validation
    {
        /// <summary>
        /// C# implementation of SQL Server's ISNULL()
        /// </summary>
        /// <param name="Value">Item to validate</param>
        /// <param name="DefaultValue">DefaultValue to return if Value is null</param>
        /// <returns>Either Value (if not null) or DefaultValue (if Value is null)</returns>
        public static object IsNull(object Value, object DefaultValue)
        {
            if (Value == null || Value == System.DBNull.Value)
            {
                return DefaultValue;
            }
            else
            {
                return Value;
            }
        }

        /// <summary>
        /// Returns a string up to a specified maximum length
        /// </summary>
        /// <param name="data">string to be modified if longer than length</param>
        /// <param name="length">maximum length of string</param>
        /// <returns>original string, or string cut to maximum length</returns>
        public static string MaxLength(string data, int length)
        {
            if (data.Length > length)
            {
                return data.Substring(0, length);
            }
            else
            {
                return data;
            }
        }

        public static string CleanInput(string raw)
        {
            var encodedInput = HttpUtility.HtmlEncode(raw);
            encodedInput = encodedInput.Replace("&#39;", "'");
            return encodedInput;
        }

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
