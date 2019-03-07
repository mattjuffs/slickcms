using System;
using System.Collections.Generic;
using System.Text;

//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SlickCMS.Data.Interfaces
{
    /// <summary>
    /// Interface specifying CRUD operations
    /// </summary>
    /// <typeparam name="IBaseEntity"></typeparam>
    public interface IEntityService<IBaseEntity> where IBaseEntity : class
    {
        void Add(IBaseEntity entity);
        void Update(IBaseEntity entity);
        void Delete(IBaseEntity entity);
        IBaseEntity Get(Expression<Func<IBaseEntity, bool>> query);
        List<IBaseEntity> GetMultiple(Expression<Func<IBaseEntity, bool>> filter, Func<IQueryable<IBaseEntity>, IOrderedQueryable<IBaseEntity>> orderBy = null, int skip = 0, int take = 10);
    }
}
