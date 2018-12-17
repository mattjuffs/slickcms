using System;
using System.Collections.Generic;

namespace SlickCMS.Data.Entities
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Ip { get; set; }
        public string HttpUserAgent { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Published { get; set; }
    }
}
