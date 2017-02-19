using KPMGAssignment.Persistence;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KPMGAssignment
{
    public class Repository : IRepository, IDisposable
    {
        private readonly AppDbContext context;

        public Repository()
        {
            context = new AppDbContext();
        }

        public Repository(AppDbContext ctx)
        {
            context = ctx;
        }


        public IQueryable<T> GetList<T>() where T : class
        {
            return context.Set<T>();
        }


        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return context.Set<T>().Where(predicate);
        }


        public T Get<T>(int id) where T : class
        {
            return context.Set<T>().Find(id);
        }


        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }


        public async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task<T> GetAsync<T>(int id) where T : class
        {
            return await context.Set<T>().FindAsync(id);
        }


        public void Add<T>(T newEntity) where T : class
        {
            context.Set<T>().Add(newEntity);
        }


        public void Remove<T>(T entity) where T : class
        {
            context.Set<T>().Remove(entity);
        }


        public void Attach<T>(T entity) where T : class
        {
            context.Set<T>().AddOrUpdate(entity);
        }


        public void Delete<T>(int id) where T : class
        {
            T dbEntry = context.Set<T>().Find(id);
            if (dbEntry != null)
            {
                context.Set<T>().Remove(dbEntry);
                context.Entry(dbEntry).State = EntityState.Deleted;
            }
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
