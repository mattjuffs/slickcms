using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Web;

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
    }
}
