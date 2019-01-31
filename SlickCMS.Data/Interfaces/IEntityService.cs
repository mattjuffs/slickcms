using System;
using System.Collections.Generic;
using System.Text;

//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SlickCMS.Data.Interfaces
{
    public interface IEntityService<IBaseEntity> where IBaseEntity : class
    {
        void Add(IBaseEntity entity);
        void Update(IBaseEntity entity);
        void Delete(IBaseEntity entity);
        IBaseEntity Get(object key);
        List<IBaseEntity> GetMultiple(Expression<Func<IBaseEntity, bool>> filter, Func<IQueryable<IBaseEntity>, IOrderedQueryable<IBaseEntity>> orderBy = null);
    }
}
