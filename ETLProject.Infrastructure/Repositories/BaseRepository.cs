using System.Linq.Expressions;
using ETLProject.Infrastructure.DatabaseContext;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Infrastructure.Repositories;

public class BaseRepository<T>: IDataRepository<T> where T : class
{
    private readonly EtlDbContext _dbContext;

    public BaseRepository(EtlDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task Create(T entity)
    {
        return _dbContext.Set<T>().AddAsync(entity).AsTask();
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return trackChanges
            ? _dbContext.Set<T>().Where(expression)
            : _dbContext.Set<T>().Where(expression).AsNoTracking();
    }

    public IQueryable<T> GetAll(bool trackChanges = false)
    {
        return trackChanges
            ? _dbContext.Set<T>()
            : _dbContext.Set<T>().AsNoTracking();
    }

    public Task SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    
}