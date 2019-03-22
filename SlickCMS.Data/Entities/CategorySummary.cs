using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public class CategorySummary
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
    }
}
