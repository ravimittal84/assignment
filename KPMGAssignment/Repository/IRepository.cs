using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KPMGAssignment
{
    public interface IRepository
    {
        IQueryable<T> GetList<T>() where T : class;

        IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Get<T>(int id) where T : class;

        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> GetAsync<T>(int id) where T : class;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        void Add<T>(T newEntity) where T : class;

        void Attach<T>(T entity) where T : class;

        void Remove<T>(T entity) where T : class;

        void Delete<T>(int id) where T : class;

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
