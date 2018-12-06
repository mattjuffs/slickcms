using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Link
    {
        public static Link Get(int linkID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Links.Where(p => p.LinkID == linkID).FirstOrDefault();
            }
        }

        public void Delete()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.ExecuteCommand("Delete From Links Where LinkID = {0}", this.LinkID);    
            }
        }

        public static List<Link> GetAll()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from l in db.Links
                    where l.Published == 1
                    orderby
                        l.Section ascending,
                        l.Name ascending
                    select l
                );

                return query.ToList();
            }
        }

        public static List<string> GetSections()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from l in db.Links
                    where l.Published == 1
                    orderby l.Section ascending
                    select l.Section
                );

                return query.Distinct().ToList();
            }
        }

        public void Insert()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Links.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }
    }
}
