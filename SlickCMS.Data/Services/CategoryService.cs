using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Data.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService() { }
        public CategoryService(SlickCMSContext context) : base(context) { }

        // TODO: list of Categories with counts (show on sidebar or a static page)
    }
}
