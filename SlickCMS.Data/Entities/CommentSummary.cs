using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public class CommentSummary
    {
        public int CommentId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string Url { get; set; }
    }
}
