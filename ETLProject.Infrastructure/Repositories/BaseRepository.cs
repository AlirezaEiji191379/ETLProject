using System.Linq.Expressions;
using ETLProject.Infrastructure.DatabaseContext;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Infrastructure.Repositories;

public abstract class BaseRepository<T>: IDataRepository<T> where T : class
{
    protected readonly EtlDbContext DbContext;

    public BaseRepository(EtlDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public abstract void Attach(T entity);

    public Task Create(T entity)
    {
        return DbContext.Set<T>().AddAsync(entity).AsTask();
    }

    public void Update(T entity)
    {
        DbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        DbContext.Set<T>().Remove(entity);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return trackChanges
            ? DbContext.Set<T>().Where(expression)
            : DbContext.Set<T>().Where(expression).AsNoTracking();
    }

    public IQueryable<T> GetAll(bool trackChanges = false)
    {
        return trackChanges
            ? DbContext.Set<T>()
            : DbContext.Set<T>().AsNoTracking();
    }

    public Task SaveChangesAsync()
    {
        return DbContext.SaveChangesAsync();
    }

    public Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return DbContext.Set<T>().AnyAsync(expression);
    }
}