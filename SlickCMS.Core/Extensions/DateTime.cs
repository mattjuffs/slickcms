using System;
using System.Collections.Generic;
using System.Text;

namespace SlickCMS.Core//.Extensions
{
    public static class DateTimeEExtensions
    {
        public static System.DateTime ToDateTime(this object theObject)
        {
            try
            {
                return Convert.ToDateTime(theObject);
            }
            catch { return new System.DateTime(); }
        }

        public static string ToHumanDate(this System.DateTime? computerDate)
        {
            return computerDate.ToDateTime().ToHumanDate();
        }

        public static string ToHumanDate(this System.DateTime computerDate)
        {
            return System.String.Format("{0:dddd d}{1} {0:MMMM} {0:yyyy}", computerDate, computerDate.Day.GetDaySuffix());
        }

        /// <summary>
        /// https://stackoverflow.com/a/9130114/63100
        /// </summary>
        public static string GetDaySuffix(this int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }

        /// <summary>
        /// Thanks to http://stackoverflow.com/a/250400/63100
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static System.DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
