using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface ICommentService : IEntityService<Comment>
    {
        List<Comment> GetPublished(int postID);
    }
}
