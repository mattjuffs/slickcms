using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class NewsItem// : IData<News>
    {
        public void Add()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.NewsItems.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = db.NewsItems.Where(p => p.NewsID == this.NewsID).FirstOrDefault();
                if (obj == null) return;

                obj.Title = this.Title;
                obj.Content = this.Content;
                obj.Published = this.Published;

                db.SubmitChanges();
            }
        }

        public void Delete()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = db.NewsItems.Where(p => p.NewsID == this.NewsID).FirstOrDefault();
                if (obj == null) return;

                db.NewsItems.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }
        }

        public static NewsItem Get(int newsID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.NewsItems.Where(p => p.NewsID == newsID).FirstOrDefault();
            }
        }

        public static List<NewsItem> GetPublished(int skip, int take)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from n in db.NewsItems
                    where n.Published == 1
                    orderby n.DateCreated descending
                    select n
                );

                return query.Skip(skip).Take(take).ToList();
            }
        }

        public static List<NewsItem> GetAll(int skip, int take)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from n in db.NewsItems
                    orderby n.DateCreated descending
                    select n
                );

                return query.Skip(skip).Take(take).ToList();
            }
        }

        public static List<NewsItem> Search(string searchQuery)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from n in db.NewsItems
                    where
                        n.Published == 1
                        && (
                            n.Title.Contains(searchQuery)
                            || n.Content.Contains(searchQuery)
                        )
                    orderby n.DateCreated descending
                    select n
                );

                return query.ToList();
            }
        }

        public static int CountPublished()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (from ni in db.NewsItems where ni.Published == 1 select ni);
                return query.Count();
            }
        }
    }
}
