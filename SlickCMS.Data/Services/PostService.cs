using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService() { }
        public PostService(SlickCMSContext context) : base(context) { }

        public List<Post> GetPublished()
        {
            // providing a where clause and orderby
            var posts = this.GetMultiple(p => p.Published == 1, q => q.OrderByDescending(r => r.DateCreated));

            // just providing a where clause
            //var posts = post.GetMultiple(p => p.Published == 1);

            return posts.ToList();
        }

        public List<Post> GetPublished(int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = this.GetMultiple(p => p.Published == 1, q => q.OrderByDescending(r => r.DateCreated), skip, take);
            return posts.ToList();
        }

        public int TotalPosts()
        {
            int totalPosts = this.GetCount(p => p.Published == 1);
            return totalPosts;
        }

        public Post GetPost(string url)
        {
            var post = this.Get(p => p.Url == url);
            return post;
        }

        public List<Post> Search(string query, int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = SearchQuery(query);
            return posts.Skip(skip).Take(take).ToList();
        }

        public int TotalSearchResults(string query)
        {
            var posts = SearchQuery(query);
            return posts.Count();
        }

        private IQueryable<Post> SearchQuery(string query)
        {
            // using IQueryable https://stackoverflow.com/a/2876655/63100

            var posts = this.GetMultiple(
                p => p.Title.Contains(query) || p.Content.Contains(query),
                q => q.OrderByDescending(r => r.DateCreated),
                0,
                1000
            );

            return posts;
        }

        private int CalculateSkip(int page, int take)
        {
            int skip = 0;

            if (page != 1)
                skip = (page * take) - take;

            return skip;
        }
    }
}
