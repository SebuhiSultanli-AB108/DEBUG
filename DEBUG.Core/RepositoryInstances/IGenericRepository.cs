using DEBUG.Core.Models;
using System.Linq.Expressions;

namespace DEBUG.Core.RepositoryInstances;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetAll(params string[]? includes);
    Task<T?> GetByIdAsync(int id, params string[]? includes);
    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, params string[]? includes);
    Task CreateAsync(T entity);
    Task HardDeleteAsync(T entity);
    void SoftDeleteAndRestore(T entity);
    Task SaveChangesAsync();
}