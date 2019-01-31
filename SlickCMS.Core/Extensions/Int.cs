﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SlickCMS.Core//.Extensions
{
    public static class IntExtensions
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
