using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface ITagService : IEntityService<Tag>
    {
        List<Tag> GetTags(int postID);
    }
}
