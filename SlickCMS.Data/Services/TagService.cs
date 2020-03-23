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

        public List<TagSummary> GetSummary(int minimumNumberOfPosts)
        {
            var query = (
                from t in this._context.Tag
                join r in this._context.Relationship on t.TagId equals r.TagId
                join p in this._context.Post on r.PostId equals p.PostId
                where p.Published == 1
                group t by t.Name into g
                where g.Count() >= minimumNumberOfPosts
                orderby
                    g.Count() descending,
                    g.Key ascending
                select new TagSummary
                {
                    Name = g.Key,
                    Count = g.Count()
                }
            );

            return query.ToList();
        }

        public List<Tag> GetTags(int postID)
        {
            var query = (
                from t in this._context.Tag
                join r in this._context.Relationship on t.TagId equals r.TagId
                join p in this._context.Post on r.PostId equals p.PostId
                where p.PostId == postID
                select t
            );

            return query.Distinct().ToList();
        }
    }
}
