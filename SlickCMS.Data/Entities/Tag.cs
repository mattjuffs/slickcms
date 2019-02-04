using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public partial class Tag : IBaseEntity
    {
        public int TagId { get; set; }
        public string Name { get; set; }
    }
}
