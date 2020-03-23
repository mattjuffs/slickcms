using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlickCMS.Web.Models
{
    /// <summary>
    /// ViewModel representing a Posts page, which comprises of multiple Posts with Pagination
    /// </summary>
    public class PostsModel
    {
        public string Name { get; set; }
        public List<SlickCMS.Data.Entities.Post> Posts { get; set; }
        public PaginationModel Pagination { get; set; }
        public PageType Type { get; set; }

        public enum PageType
        {
            Post,
            Search,
            Category,
            Tag
        }
    }
}
