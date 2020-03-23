using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Announcement
    {
        public Extension.MessageType MessageType
        {
            get
            {
                switch (this.Type)
                {
                    case "Success":
                        return Extension.MessageType.Success;
                    case "Warning":
                        return Extension.MessageType.Warning;
                    case "Error":
                        return Extension.MessageType.Error;
                    case "Information":
                        return Extension.MessageType.Information;
                    default:
                        return Extension.MessageType.Information;
                }
            }
        }

        public static Announcement Get()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from a in db.Announcements
                    where a.Active
                    orderby a.DateCreated descending
                    select a
                );

                return query.FirstOrDefault();
            }
        }

        public static List<Announcement> GetLast5()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from a in db.Announcements
                    where !a.Active
                    orderby a.AnnouncementID descending
                    select a
                );

                return query.Take(5).ToList();
            }
        }

        public static void MakeInactive()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from a in db.Announcements
                    where a.Active
                    select a
                );

                foreach (Announcement item in query)
                {
                    item.Active = false;
                }

                db.SubmitChanges();
            }
        }

        public void Add()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Announcements.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }
    }
}
