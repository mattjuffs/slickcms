using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlickCMS.Web.Models
{
    public class PaginationModel
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPosts { get; set; }
        public string Query { get; set; }
    }
}
