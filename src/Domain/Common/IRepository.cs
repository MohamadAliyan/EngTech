using System.Linq.Expressions;

namespace EngTech.Domain.Common;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get();
    Task<int> SaveChanges();
}



