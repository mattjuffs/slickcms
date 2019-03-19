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
            return this.GetMultiple(c => c.Published == 1 && c.PostId == postID, d => d.OrderBy(e => e.DateCreated));
        }
    }
}
