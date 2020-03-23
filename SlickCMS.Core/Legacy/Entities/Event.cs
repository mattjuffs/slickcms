using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SlickCMS
{
    public partial class Event
    {
        public void Add()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Events.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }

        public static Event Get(int eventID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Events.Where(e => e.EventID == eventID).FirstOrDefault();
            }
        }

        public static string GetEventContent(int eventID)
        {
            string cacheKey = "GetEventContent_" + eventID.ToString();

            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                using (SlickCMSDataContext db = SlickCMSDataContext.Create())
                {
                    db.ObjectTrackingEnabled = false;

                    var query = (
                        from e in db.Events
                        where e.EventID == eventID
                        select e.Content
                    );

                    HttpContext.Current.Cache[cacheKey] = (query.FirstOrDefault() ?? "");
                }
            }
            
            return HttpContext.Current.Cache[cacheKey].ToString();
        }

        public void Delete()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                Event obj = (from e in db.Events where e.EventID == this.EventID select e).FirstOrDefault();
                db.Events.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }
        }

        public static List<Event> GetAll()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from e in db.Events
                    where e.StartDate > GetTermStart()
                    orderby e.StartDate ascending
                    select e
                );

                return query.ToList();
            }
        }

        public static List<Event> GetAll(DateTime fromDate, DateTime toDate)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from e in db.Events
                    where
                        e.StartDate >= fromDate
                        && e.EndDate <= toDate
                    select e
                );

                return query.ToList();
            }
        }

        /// <summary>
        /// Retrieves the next x Events for rendering in Diary
        /// </summary>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<Event> GetDiaryEvents(int take)
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from e in db.Events
                    where e.StartDate >= start || e.EndDate >= start
                    orderby e.StartDate ascending
                    select e
                );

                return query.Take(take).ToList();
            }
        }

        /// <summary>
        /// Determines when the first of September was this term
        /// </summary>
        /// <returns></returns>
        public static DateTime GetTermStart()
        {
            DateTime now = DateTime.Now;

            if (now.Month < 9)
                return new DateTime(now.Year - 1, 9, 1, 0, 0, 0);
            else
                return new DateTime(now.Year, 9, 1, 0, 0, 0);
        }
    }
}
