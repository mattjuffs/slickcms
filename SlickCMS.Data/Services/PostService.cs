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

            return posts;
        }

        public List<Post> GetPublished(int page, int take)
        {
            int skip = 0;

            if (page != 1)
                skip = (page * take) - take;

            var posts = this.GetMultiple(p => p.Published == 1, q => q.OrderByDescending(r => r.DateCreated), skip, take);

            return posts;
        }

        public Post GetPost(string url)
        {
            var post = this.Get(p => p.Url == url);

            return post;
        }

        public int TotalPosts()
        {
            int totalPosts = this.GetCount(p => p.Published == 1);
            return totalPosts;
        }
    }
}
