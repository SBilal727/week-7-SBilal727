using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Reflection.Metadata;

namespace ClassSchedule.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ClassScheduleContext context { get; set; }
        private DbSet<T> dbset { get; set; }

        public Repository(ClassScheduleContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes()) {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);

            // #34. Update the code that handles ordering so it uses the ThenOrderBy property

            if (options.HasOrderBy)
            {
                if (options.HasThenOrderBy)
                {
                    query = query.OrderBy(options.OrderBy).ThenBy(options.ThenOrderBy);
                }
                else
                {
                    query = query.OrderBy(options.OrderBy);
                }
            }
            return query.ToList();
        }

        public virtual T? Get(int id) => dbset.Find(id);


        // #39. Open the Repository<T> class and implement the new Get() method.
        // Return the single object by calling the FirstOrDefault() method instead of the List() method.

        public virtual T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);


            return query.FirstOrDefault();
        }

        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }

}
