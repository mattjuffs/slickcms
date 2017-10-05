using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core.Extensions
{
    public static class Int
    {
        /// <summary>
        /// Converts an integer into a boolean
        /// </summary>
        /// <param name="obj">Original int</param>
        /// <returns>True if 1, otherwise False</returns>
        public static bool ToBool(this int obj)
        {
            if (obj == 1)
                return true;
            else
                return false;
        }
    }
}
