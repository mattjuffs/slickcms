using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core
{
    public static class Extension
    {
        public static DateTime ToDateTime(this object theObject)
        {
            try
            {
                return Convert.ToDateTime(theObject);
            }
            catch { return new DateTime(); }
        }

        public static string ToHumanDate(this DateTime? computerDate)
        {
            return ToHumanDate(computerDate.ToDateTime());
        }

        public static string ToHumanDate(this DateTime computerDate)
        {
            string humanDate = String.Format("{0:dddd d}{1} {0:MMMM} {0:yyyy}", computerDate, computerDate.Day.GetDaySuffix());
            return humanDate;
        }

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
    }
}
