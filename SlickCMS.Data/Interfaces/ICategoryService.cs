using System;
using System.Collections.Generic;
using System.Text;
using SlickCMS.Data.Entities;

namespace SlickCMS.Data.Interfaces
{
    public interface ICategoryService : IEntityService<Category>
    {
        List<CategorySummary> GetSummary(string type);
        List<Category> GetCategories(int postID);
    }
}
