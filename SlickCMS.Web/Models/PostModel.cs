using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlickCMS.Web.Models
{
    /// <summary>
    /// ViewModel representing a Post page, which comprises of a Post with multiple Comments
    /// </summary>
    public class PostModel
    {
        public SlickCMS.Data.Entities.Post Post { get; set; }
        public List<SlickCMS.Data.Entities.Comment> Comments { get; set; }
        public List<SlickCMS.Data.Entities.Category> Categories { get; set; }
        public List<SlickCMS.Data.Entities.Tag> Tags { get; set; }
    }
}
