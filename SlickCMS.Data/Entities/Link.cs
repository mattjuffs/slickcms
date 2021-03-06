﻿using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public partial class Link : IBaseEntity
    {
        public int LinkId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Published { get; set; }
    }
}
