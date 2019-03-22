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

        public List<CategorySummary> GetSummary(string type)
        {
            var query = (
                from c in this._context.Category
                join r in this._context.Relationship on c.CategoryId equals r.CategoryId
                join p in this._context.Post on r.PostId equals p.PostId
                where
                    c.Type == type
                    && p.Published == 1
                group c by new
                {
                    c.Name,
                    c.Description
                } into g
                select new CategorySummary
                {
                    Name = g.Key.Name,
                    Description = g.Key.Description,
                    Count = g.Count()
                }
            );

            return query.ToList();
        }

        public List<Category> GetCategories(int postID)
        {
            var query = (
                from c in this._context.Category
                join r in this._context.Relationship on c.CategoryId equals r.CategoryId
                join p in this._context.Post on r.PostId equals p.PostId
                where p.PostId == postID
                select c
            );

            return query.ToList();
        }
    }
}
