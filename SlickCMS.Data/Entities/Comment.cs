using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Comment : IData<Comment>
    {
        /// <summary>
        /// Inserts a new Comment
        /// </summary>
        public void Insert()
        {
            //set system generated items:
            /*this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Comments.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();*/

            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Comments.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Selects a single Comment
        /// </summary>
        /// <param name="id">Unique ID of Comment</param>
        /// <returns>Comment</returns>
        public Comment Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Comments.SingleOrDefault(c => c.CommentID == id);
        }

        /// <summary>
        /// Selects a List of Comments
        /// </summary>
        /// <param name="skip">Number of Comments to skip</param>
        /// <param name="take">Number of Comments to select</param>
        /// <returns>A List of Comments</returns>
        public List<Comment> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from c in dc.Comments
                orderby c.CommentID
                select c
            );

            //TODO: implement search

            return query.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Updates an existing Comment
        /// </summary>
        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            Comment comment = dc.Comments.Single(c => c.CommentID == this.CommentID);

            comment.PostID = this.PostID;
            comment.UserID = this.UserID;
            comment.Name = this.Name;
            comment.Email = this.Email;
            comment.URL = this.URL;
            comment.IP = this.IP;
            comment.HTTP_USER_AGENT = this.HTTP_USER_AGENT;
            comment.Content = this.Content;
            comment.DateModified = DateTime.Now;
            comment.Published = this.Published;

            dc.SubmitChanges();
            dc.Dispose();
        }

        /// <summary>
        /// Deletes an existing Comment
        /// </summary>
        public void Delete()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Comments.DeleteOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        public static Comment Get(int commentID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Comments.Where(c => c.CommentID == commentID).FirstOrDefault();
            }
        }

        public static List<Comment> GetAll(int postID, int published)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from c in db.Comments
                    where c.Published == published
                    select c
                );

                if (postID > 0)
                    query = query.Where(p => p.PostID == postID);

                return query.OrderByDescending(p => p.DateCreated).ToList();
            }
        }
    }
}
