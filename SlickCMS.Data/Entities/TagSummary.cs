using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public partial class TagSummary
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
