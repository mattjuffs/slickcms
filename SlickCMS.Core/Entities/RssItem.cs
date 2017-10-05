using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlickCMS.Core.Entities
{
    public class RssItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
