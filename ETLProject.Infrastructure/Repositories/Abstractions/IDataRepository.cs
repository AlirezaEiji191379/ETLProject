using System.Linq.Expressions;

namespace ETLProject.Infrastructure.Repositories.Abstractions;

public interface IDataRepository<T> where T : class
{
    void Attach(T entity);
    Task Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> GetAll(bool trackChanges = false);
    Task SaveChangesAsync();
    Task<bool> Any(Expression<Func<T, bool>> expression);
}