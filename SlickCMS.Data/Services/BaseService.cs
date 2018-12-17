using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Collections.Generic;
//using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace SlickCMS.Data.Services
{
    /// <summary>
    /// Contains generic Data methods for CRUD operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> where TEntity : class
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

        public virtual void Add(TEntity entity)
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

            // TODO: add entity to context
            _context.Add<TEntity>(entity);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
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

        public virtual void Delete(TEntity entity)
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

        public virtual TEntity Get(object key)
        {
            /*using (var db = CreateContext())
            {
                var table = db.Set<TEntity>();
                return table.Find(key);
            }*/

            // TODO: get entity from context
            var table = _context.Set<TEntity>();
            return table.Find(key);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> query)
        {
            /*using (var db = CreateContext())
            {
                var table = db.Set<TEntity>();
                return table.Where(query).FirstOrDefault();
            }*/

            // TODO: get entity from context
            var table = _context.Set<TEntity>();
            return table.Where(query).FirstOrDefault();
        }

        public virtual List<TEntity> GetMultiple(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
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

            // TODO: get entities from context
            var table = _context.Set<TEntity>();
            var query = table.Where(filter);

            if (orderBy != null)
                return orderBy(query).ToList();
            
            return query.ToList();
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
