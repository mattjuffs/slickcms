using System;
using System.Collections.Generic;

namespace SlickCMS.Data.Entities
{
    public partial class Category //: BaseService<Category>
    {
        //public Category() { }
        //public Category(SlickCMSContext context) : base(context) { }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
