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
        public List<SlickCMS.Data.Entities.Post> Posts { get; set; }
        public PaginationModel Pagination { get; set; }
    }
}
