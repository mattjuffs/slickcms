using System;
using System.Collections.Generic;

namespace SlickCMS.Data
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Search { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Published { get; set; }
        public int Pageable { get; set; }
    }
}
