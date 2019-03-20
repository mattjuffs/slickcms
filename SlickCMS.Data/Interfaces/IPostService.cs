﻿using System;
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
        List<Post> GetPublished(int page, int take);
        Post GetPost(string url);
        int TotalPosts();
        int TotalSearchResults(string query);
        List<Post> Search(string query, int page, int take);
    }
}
