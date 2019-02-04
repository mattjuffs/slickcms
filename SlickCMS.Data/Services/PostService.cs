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
    }
}
