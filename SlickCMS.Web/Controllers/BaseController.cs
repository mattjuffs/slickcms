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

            LoadCategories();
            LoadTags();
        }

        /// <summary>
        /// Loads the Categories into Cache, for use on the navigation
        /// </summary>
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

        /// <summary>
        /// Loads the Tags into Cache, for use on the navigation
        /// </summary>
        public void LoadTags()
        {
            string cacheKey = "Tags";

            var cachedTags = SlickCMS.Core.Caching.MemoryCache.Get<List<SlickCMS.Data.Entities.TagSummary>>(cacheKey);

            if (cachedTags == null)
            {
                var tags = GetTags();
                SlickCMS.Core.Caching.MemoryCache.Add(cacheKey, tags);
            }
        }

        private List<SlickCMS.Data.Entities.TagSummary> GetTags()
        {
            var tagService = new SlickCMS.Data.Services.TagService(this._context);
            return tagService.GetSummary(2);
        }
    }
}