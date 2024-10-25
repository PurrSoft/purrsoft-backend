using System.Linq.Expressions;

namespace PurrSoft.Domain.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query(Expression<Func<T, bool>> whereQueryFilter = null);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}
