using System;
using System.Collections.Generic;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    /// <summary>
    /// Interface for PostService
    /// </summary>
    public interface IPostService : IEntityService<Post>
    {
        List<Post> GetPublished();
    }
}
