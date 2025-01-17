using Microsoft.EntityFrameworkCore;
using PurrSoft.Domain.Repositories;
using System.Linq.Expressions;

namespace PurrSoft.Persistence.Repositories;

public class Repository<T>(PurrSoftDbContext context) : IRepository<T> where T : class
{
    protected PurrSoftDbContext DbContext = context;

    public void Add(T entity) => DbContext.Add(entity);

    public void AddRangeAsync(ICollection<T> entities) => DbContext.AddRangeAsync(entities);

    public IQueryable<T> Query(Expression<Func<T, bool>> whereFilter = null)
    {
        DbSet<T> query = DbContext.Set<T>();
        return whereFilter != null ? query.Where(whereFilter) : query;
    }

    public void Remove(T entity) => DbContext.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbContext.RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await DbContext.SaveChangesAsync(cancellationToken);
}
