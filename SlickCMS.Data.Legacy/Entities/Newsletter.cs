using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Newsletter
    {
        public static List<Newsletter> GetAll()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from n in db.Newsletters
                    orderby n.DateSent descending
                    select n
                );

                return query.ToList();
            }
        }

        public static Newsletter Get(int newsletterID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Newsletters.Where(p => p.NewsletterID == newsletterID).FirstOrDefault();
            }
        }

        public void Add()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Newsletters.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                Newsletter obj = (from n in db.Newsletters where n.NewsletterID == this.NewsletterID select n).FirstOrDefault();

                obj.Volume = this.Volume;
                obj.Issue = this.Issue;
                obj.DateSent = this.DateSent;
                obj.PDF = this.PDF;
                obj.Summary = this.Summary;

                db.SubmitChanges();
            }
        }

        public void Delete()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                Newsletter obj = (from n in db.Newsletters where n.NewsletterID == this.NewsletterID select n).FirstOrDefault();
                db.Newsletters.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }
        }
    }
}
