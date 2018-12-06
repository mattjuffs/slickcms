using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core.Legacy//.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string into a string array by splitting on the delimiter
        /// </summary>
        /// <param name="obj">string to split into array</param>
        /// <param name="delimiter">delimiter/separator within string</param>
        /// <returns>a new string array</returns>
        public static string[] ToArray(this string obj, char delimiter)
        {
            string[] returnArray;

            if (!string.IsNullOrEmpty(obj) && obj.Contains(delimiter))
            {
                returnArray = obj.Split(delimiter);
            }
            else
            {
                // just add the one element to the array
                returnArray = new string[] { obj };
            }

            return returnArray;
        }

        /// <summary>
        /// Ensures a String is only as long as a specified length
        /// </summary>
        /// <param name="obj">Original string</param>
        /// <param name="length">Maximum length of string</param>
        /// <returns>Either original string, or substring if longer than length</returns>
        public static string MaxLength(this string obj, int length)
        {
            if (obj.Length > length)
            {
                return obj.Substring(0, length);
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// Converts a String into a boolean
        /// </summary>
        /// <param name="obj">Original string</param>
        /// <returns>True if "1", otherwise false</returns>
        public static bool ToBool(this string obj)
        {
            if (obj == "1")
                return true;
            else
                return false;
        }
    }
}
