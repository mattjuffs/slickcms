using System;
using System.Collections.Generic;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Entities
{
    public partial class Image : IBaseEntity
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Orientation { get; set; }
        public DateTime Uploaded { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
    }
}
