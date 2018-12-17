using System;
using System.Collections.Generic;

namespace SlickCMS.Data.Entities
{
    public partial class Relationship
    {
        public int RelationshipId { get; set; }
        public int CategoryId { get; set; }
        public int LinkId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int TagId { get; set; }
        public int Order { get; set; }
    }
}
