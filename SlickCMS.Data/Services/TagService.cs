using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class TagService : BaseService<Tag>, ITagService
    {
        public TagService() { }
        public TagService(SlickCMSContext context) : base(context) { }

        // TODO: list of Tags with counts (show on sidebar or a static page)
    }
}
