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
            var posts = this.GetMultiple(p => p.Published == 1 && p.Pageable == 1, q => q.OrderByDescending(r => r.DateCreated));

            // just providing a where clause
            //var posts = post.GetMultiple(p => p.Published == 1);

            return posts.ToList();
        }

        public List<Post> GetPublished(int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = this.GetMultiple(p => p.Published == 1 && p.Pageable == 1, q => q.OrderByDescending(r => r.DateCreated), skip, take);
            return posts.ToList();
        }

        public int TotalPosts()
        {
            int totalPosts = this.GetCount(p => p.Published == 1 && p.Pageable == 1);
            return totalPosts;
        }

        public int TotalPostsForAdmin()
        {
            int totalPosts = this.GetCount(p => p.PostId > -10);
            return totalPosts;
        }

        public List<Post> GetForAdmin(int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = this.GetMultiple(p => p.PostId > -10, q => q.OrderByDescending(r => r.PostId), skip, take);
            return posts.ToList();
        }

        public Post GetPost(string url)
        {
            var post = this.Get(p => p.Url == url);
            return post;
        }

        public Post GetPost(int id)
        {
            var post = this.Get(p => p.PostId == id);

            if (post != null)
                return post;

            return new Post
            {
                PostId = 0,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Title = "",
                Summary = "",
                Content = "",
                Published = 0,
                Search = "",
                Url = "",
                UserId = 0,                
            };
        }

        public List<Post> Search(string query, int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = SearchQuery(query);
            return posts.Distinct().OrderByDescending(o => o.DateCreated).Skip(skip).Take(take).ToList();
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
                p => p.Title.Contains(query)
                    || p.Content.Contains(query)
                    || p.Search.Contains(query),
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

        public List<Post> Category(string name, int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = CategoryQuery(name);
            return posts.Distinct().Skip(skip).Take(take).ToList();
        }

        public int TotalCategoryPosts(string name)
        {
            var posts = CategoryQuery(name);
            return posts.Count();
        }

        private IQueryable<Post> CategoryQuery(string name)
        {
            var query = (
                from p in this._context.Post
                join r in this._context.Relationship on p.PostId equals r.PostId
                join c in this._context.Category on r.CategoryId equals c.CategoryId
                where
                    p.Published == 1
                    && c.Type == "Posts"
                    && c.Name == name
                orderby p.DateCreated descending
                select p
            );

            return query;
        }

        public List<Post> Tag(string name, int page, int take)
        {
            int skip = CalculateSkip(page, take);
            var posts = TagQuery(name);
            return posts.Distinct().Skip(skip).Take(take).ToList();
        }

        public int TotalTagPosts(string name)
        {
            var posts = TagQuery(name);
            return posts.Count();
        }

        private IQueryable<Post> TagQuery(string name)
        {
            var query = (
                from p in this._context.Post
                join r in this._context.Relationship on p.PostId equals r.PostId
                join t in this._context.Tag on r.TagId equals t.TagId
                where
                    p.Published == 1
                    && t.Name == name
                orderby p.DateCreated descending
                select p
            );

            return query;
        }
    }
}
