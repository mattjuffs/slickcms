using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        public CommentService() { }
        public CommentService(SlickCMSContext context) : base(context) { }

        public List<Comment> GetPublished(int postID)
        {
            return this.GetMultiple(c => c.Published == 1 && c.PostId == postID, d => d.OrderBy(e => e.DateCreated), 0, 1000).ToList();
        }

        public List<CommentSummary> GetLatestPublished(int take)
        {
            var query = (
                from c in this._context.Comment
                join p in this._context.Post on c.PostId equals p.PostId
                where
                    p.Published == 1
                    && c.Published == 1
                orderby c.DateCreated descending
                select new SlickCMS.Data.Entities.CommentSummary
                {
                    Name = c.Name,
                    Content = c.Content,
                    DateCreated = c.DateCreated,
                    Url = p.Url
                }
            );

            return query.Skip(0).Take(take).ToList();
            
            //return this.GetMultiple(c => c.Published == 1, d => d.OrderBy(e => e.DateCreated), 0, take).ToList();
        }
    }
}
