using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Application.EventHandlers.Core.Repository
{
    public interface IGenericRepository<T>
        where T : class, new()
    {
        IEnumerable<T> GetAll();
        T Single(params object[] llavePrimaria);
        T Single(Expression<Func<T, bool>> predicate, bool tracking = true);
        IList<T> Search(Expression<Func<T, bool>> criterio, bool tracking = true);
        IList<T> Search(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes, bool tracking = true);
        bool Exists(params object[] llavePrimaria);
    }

    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        internal DbSet<T> dbSet;

        public GenericRepository(DbContext context)
        {
            this.dbSet = context.Set<T>();
        }

        public bool Exists(params object[] llavePrimaria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll() => dbSet.ToList();

        public IList<T> Search(Expression<Func<T, bool>> criterio, bool tracking = true)
        {
            var query = dbSet.Where(criterio);

            if (!tracking) query = query.AsNoTracking();

            return query.ToList();
        }

        public IList<T> Search(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes, bool tracking = true)
        {
            var includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            DbQuery<T> query = dbSet;
            includelist.ForEach(x => query = query.Include(x));
            return query.Where(predicate).ToList();
        }

        public T Single(params object[] llavePrimaria)
            => dbSet.Find(llavePrimaria);

        public T Single(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = dbSet.Where(predicate);

            if (!tracking) query = query.AsNoTracking();

            return query.Single();
        }
    }
}
