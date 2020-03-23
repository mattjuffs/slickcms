using System;
using System.Collections.Generic;
using System.Text;

namespace SlickCMS.Core
{
    public class Random
    {
        /// <summary>
        /// Creates a string of random text for a specified length
        /// </summary>
        /// <param name="length">Length of random text to generate</param>
        /// <returns>String with length of length</returns>
        public static string RandomText(int length)
        {
            var s = new StringBuilder();

            s.Append(GetGuid());

            while (s.Length < length)
            {
                s.Append(GetGuid(true));
            }

            return s.ToString().Substring(0, length);
        }

        /// <summary>
        /// Gets a GUID as a string
        /// </summary>
        /// <returns>A single GUID</returns>
        private static string GetGuid(bool stripDashes)
        {
            var guid = System.Guid.NewGuid();

            if (stripDashes)
            {
                return guid.ToString().Replace("-", "");
            }
            else
            {
                return guid.ToString();
            }
        }

        public static string GetGuid()
        {
            return GetGuid(false);
        }
    }
}
