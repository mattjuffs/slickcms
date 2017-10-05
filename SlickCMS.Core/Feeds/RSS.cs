using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

namespace SlickCMS.Core.Feeds
{
    public class RSS
    {
        // combined rss feed of all items
        public static void Output(System.Web.HttpContext context)
        {
            HttpResponse response = context.Response;// alias

            response.Clear();
            response.ContentType = "text/xml";

            XmlTextWriter feedWriter = new XmlTextWriter(response.OutputStream, Encoding.UTF8);

            feedWriter.WriteStartDocument();

            feedWriter.WriteStartElement("rss");
            feedWriter.WriteAttributeString("version", "2.0");

            feedWriter.WriteStartElement("channel");
            feedWriter.WriteElementString("title", "Example.com");
            feedWriter.WriteElementString("link", "http://www.example.com/");
            feedWriter.WriteElementString("description", "Example.com RSS Feed");
            feedWriter.WriteElementString("copyright", string.Format("Copyright {0} Example.com", DateTime.Now.Year));

            List<Entities.RssItem> items = GetAllBuffered();

            // write out all rss items
            foreach (Entities.RssItem item in items)
            {
                feedWriter.WriteStartElement("item");
                feedWriter.WriteElementString("title", item.Title);
                feedWriter.WriteElementString("description", item.Description);
                feedWriter.WriteElementString("link", item.Link);
                feedWriter.WriteElementString("pubDate", item.PublishedDate.ToString());
                feedWriter.WriteEndElement();
            }

            feedWriter.WriteEndElement();
            feedWriter.WriteEndElement();
            feedWriter.WriteEndDocument();
            feedWriter.Flush();
            feedWriter.Close();

            response.End();
        }

        public static List<Entities.RssItem> GetAllBuffered()
        {
            return GetAll();

            /*string cacheKey = "RssItems";

            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                HttpContext.Current.Cache.Insert(
                    cacheKey,
                    GetAll(),
                    null,
                    DateTime.Now.AddHours(1),
                    System.Web.Caching.Cache.NoSlidingExpiration
                );
            }

            return (List<RssItem>)HttpContext.Current.Cache.Get(cacheKey);*/
        }

        private static List<Entities.RssItem> GetAll()
        {
            var items = new List<Entities.RssItem>();
            return items;

            /*using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var postsQuery = (
                    from p in db.Posts
                    where
                        p.Published == 1
                        && p.Deleted == false
                        && p.Archive == null// excluding archived posts, as they're no longer active
                    select new Entities.RssItem
                    {
                        Title = "Page: " + p.Title,
                        Description = p.Content,
                        Link = "http://www.example.com/pages/" + p.URL + ".aspx",
                        Guid = "POST" + p.PostID,
                        PublishedDate = p.DateCreated,
                    }
                );
                items.AddRange(postsQuery);

                var newsQuery = (
                    from n in db.NewsItems
                    where n.Published == 1
                    select new Entities.RssItem
                    {
                        Title = "News: " + n.Title,
                        Description = n.Content,
                        Link = "http://www.example.com/pages/news-and-events/news.aspx",
                        Guid = "NEWS" + n.NewsID,
                        PublishedDate = n.DateCreated,
                    }
                );
                items.AddRange(newsQuery);

                var eventsQuery = (
                    from e in db.Events
                    select new Entities.RssItem
                    {
                        Title = "Event: " + e.Title,
                        Description = e.Content,
                        Link = "http://www.example.com/pages/our-school/calendar.aspx#event" + e.EventID,
                        Guid = "EVENT" + e.EventID,
                        PublishedDate = e.StartDate,
                    }
                );
                items.AddRange(eventsQuery);

                var newslettersQuery = (
                    from n in db.Newsletters
                    select new Entities.RssItem
                    {
                        Title = String.Format("Newsletter: Volume {0} Issue {1}", n.Volume, n.Issue),
                        Description = n.Summary,
                        Link = "http://www.example.com/documents/newsletters/" + n.PDF,
                        Guid = "NEWSLETTER" + n.NewsletterID,
                        PublishedDate = n.DateSent,
                    }
                );
                items.AddRange(newslettersQuery);
            }

            // not interested in future items
            items = items.Where(p => p.PublishedDate <= DateTime.Now).ToList();

            // show the newest first
            items = items.OrderByDescending(p => p.PublishedDate).ToList();

            return items;*/
        }
    }
}
