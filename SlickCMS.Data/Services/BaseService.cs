using System;
using System.Collections.Generic;
using System.Text;

//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SlickCMS.Data.Services
{
    /// <summary>
    /// Contains generic Data methods for CRUD operations
    /// </summary>
    /// <typeparam name="IBaseEntity"></typeparam>
    public class BaseService<IBaseEntity> where IBaseEntity : class
    {
        public BaseService() { }

        public readonly SlickCMSContext _context;
        public BaseService(SlickCMSContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Specifies whether to use EntityState for CRUD operations
        /// </summary>
        /*private bool useEntityState
        {
            get { return true; }
        }*/

        public virtual void Add(IBaseEntity entity)
        {
            /*using (var db = CreateContext())
            {
                if (useEntityState)
                {
                    db.Entry(entity).State = System.Data.EntityState.Added;
                }
                else
                {
                    db.Set<TEntity>().Add(entity);
                }

                db.SaveChanges();
            }*/

            _context.Add<IBaseEntity>(entity);
            _context.SaveChanges();
        }

        public virtual void Update(IBaseEntity entity)
        {
            /*using (var db = CreateContext())
            {
                if (useEntityState)
                {
                    db.Entry(entity).State = System.Data.EntityState.Modified;
                }
                else
                {
                    db.Set<TEntity>().Attach(entity);
                    var manager = ((IObjectContextAdapter)db).ObjectContext.ObjectStateManager;
                    manager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                }

                db.SaveChanges();
            }*/

            // TODO: update entity within context
        }

        public virtual void Delete(IBaseEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            /*using (var db = CreateContext())
            {
                if (useEntityState)
                {
                    db.Entry<TEntity>(entity).State = System.Data.EntityState.Deleted;
                }
                else
                {
                    db.Set<TEntity>().Attach(entity);
                    db.Set<TEntity>().Remove(entity);
                }

                db.SaveChanges();
            }*/

            // TODO: delete entity within context (flag it as deleted)
        }

        public virtual IBaseEntity Get(object key)
        {
            /*using (var db = CreateContext())
            {
                var table = db.Set<TEntity>();
                return table.Find(key);
            }*/

            var table = _context.Set<IBaseEntity>();
            return table.Find(key);
        }

        public virtual IBaseEntity Get(Expression<Func<IBaseEntity, bool>> query)
        {
            /*using (var db = CreateContext())
            {
                var table = db.Set<TEntity>();
                return table.Where(query).FirstOrDefault();
            }*/

            var table = _context.Set<IBaseEntity>();
            return table.Where(query).FirstOrDefault();
        }

        public virtual int GetCount(Expression<Func<IBaseEntity, bool>> query)
        {
            var table = _context.Set<IBaseEntity>();
            return table.Where(query).Count();
        }

        public virtual List<IBaseEntity> GetMultiple(Expression<Func<IBaseEntity, bool>> filter, Func<IQueryable<IBaseEntity>, IOrderedQueryable<IBaseEntity>> orderBy = null, int skip = 0, int take = 10)
        {
            /*using (var db = CreateContext())
            {
                var table = db.Set<TEntity>();
                var query = table.Where(filter);

                if (orderBy != null)
                    return orderBy(query).ToList();
                else
                    return query.ToList();
            }*/

            var table = _context.Set<IBaseEntity>();
            var query = table.Where(filter);

            if (orderBy != null)
                return orderBy(query).Skip(skip).Take(take).ToList();

            return query.Skip(skip).Take(take).ToList();
        }

        /*internal virtual SlickCMSContext CreateContext()
        {
            var context = new SlickCMSContext();

            var adapter = (IObjectContextAdapter)context;
            var objectContext = adapter.ObjectContext;
            objectContext.CommandTimeout = 60;

            // NOTE: other properties can be set here prior to returning context

            return context;
        }*/
    }
}
