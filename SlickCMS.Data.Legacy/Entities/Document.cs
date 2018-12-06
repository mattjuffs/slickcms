using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Document : IData<Document>
    {
        public void Insert()
        {
            this.Uploaded = DateTime.Now;

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Documents.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        public Document Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Documents.SingleOrDefault(d => d.DocumentID == id);
        }

        public List<Document> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from d in dc.Documents select d).ToList();
        }

        public List<Document> SelectMultiple(int propertyID)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from d in dc.Documents where d.PropertyID == propertyID orderby d.Type ascending select d).ToList();
        }

        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            Document document = dc.Documents.Single(d => d.DocumentID == this.DocumentID);
            document.Name = this.Name;
            document.Extension = this.Extension;
            document.Caption = this.Caption;
            document.PropertyID = this.PropertyID;
            document.Type = this.Type;
            document.Published = this.Published;
            document.Rank = this.Rank;

            dc.SubmitChanges();
            dc.Dispose();
        }

        public void Delete()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.ExecuteCommand("Delete From Documents Where DocumentID = {0}", this.DocumentID);
            dc.Dispose();
        }

        public static List<Document> GetPostDocuments(int postID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from d in db.Documents
                    where d.PostID == postID
                    orderby
                        d.Rank ascending,
                        d.Uploaded descending
                    select d
                );

                return query.ToList();
            }
        }

        public static List<Document> GetDocuments(string documentType)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from d in db.Documents
                    where
                        d.PostID == 0
                        && d.Type == documentType
                    orderby d.Uploaded descending
                    select d
                );

                return query.ToList();
            }
        }

        public static List<Document> Search(string searchQuery)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from d in db.Documents
                    where
                        d.Name.Contains(searchQuery)
                        || d.Caption.Contains(searchQuery)
                    select d
                );

                return query.ToList();
            }
        }

        public static List<string> GetTypes()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from d in db.Documents
                    orderby d.Type ascending
                    select d.Type
                );

                return query.Distinct().ToList();
            }
        }

        public void RankUp()
        {
            this.Rank--;
            this.Update();
        }

        public void RankDown()
        {
            this.Rank++;
            this.Update();
        }
    }
}
