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

        // TODO
    }
}
