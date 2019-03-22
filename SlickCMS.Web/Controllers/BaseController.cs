using Microsoft.AspNetCore.Mvc;
using SlickCMS.Data;
using System.Collections.Generic;

namespace SlickCMS.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly SlickCMSContext _context;

        public BaseController(SlickCMSContext context)
        {
            this._context = context;
        }

        public void LoadCategories()
        {
            string cacheKey = "Categories|Posts";

            var cachedCategories = SlickCMS.Core.Caching.MemoryCache.Get<List<SlickCMS.Data.Entities.CategorySummary>>(cacheKey);

            if (cachedCategories == null)
            {
                var categories = GetCategories();
                SlickCMS.Core.Caching.MemoryCache.Add(cacheKey, categories);
            }
        }

        private List<SlickCMS.Data.Entities.CategorySummary> GetCategories()
        {
            var categoryService = new SlickCMS.Data.Services.CategoryService(this._context);
            return categoryService.GetSummary("Posts");
        }
    }
}