using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

using SlickCMS.Data.Services;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface IPostService : IEntityService<Post>
    {
        List<Post> GetPublished();
    }
}
