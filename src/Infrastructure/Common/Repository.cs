using System.Linq.Expressions;
using EngTech.Domain.Common;
using EngTech.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EngTech.Infrastructure.Common;

public class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;
    private string errorMessage = string.Empty;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;

        _entity = context.Set<T>();
    }

    public virtual IQueryable<T> GetAll()
    {
        return _entity;
    }

    public void Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }


        _entity.Add(entity);
    }

    public void Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
    }

    public void Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }


        _entity.Remove(entity);
    }

    public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return _entity.Where(predicate);
    }

    public IQueryable<T> Get()
    {
        return _entity.AsQueryable();
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
}
