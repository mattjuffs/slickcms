using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class PostService : BaseService<Post>, IPostService
    {
        public PostService() { }
        public PostService(SlickCMSContext context) : base(context) { }

        /*private readonly SlickCMSContext _context;
        public PostService(SlickCMSContext context)
        {
            _context = context;
        }*/

        public List<Post> GetPublished()
        {
            //var post = new SlickCMS.Data.Entities.Post(_context);

            // providing a where clause and orderby
            var posts = this.GetMultiple(p => p.Published == 1, q => q.OrderByDescending(r => r.DateCreated));

            // just providing a where clause
            //var posts = post.GetMultiple(p => p.Published == 1);

            return posts;
        }
    }
}
