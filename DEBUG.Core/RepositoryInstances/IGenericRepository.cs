using DEBUG.Core.Entities;
using System.Linq.Expressions;

namespace DEBUG.Core.RepositoryInstances;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync(params string[]? includes);
    Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? where = null, params string[]? includes);
    Task<IEnumerable<T>?> GetRangeByIdsAsync(int[] ids, Expression<Func<T, bool>>? where = null, params string[]? includes);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, params string[]? includes);
    Task CreateAsync(T entity);
    Task HardDeleteAsync(T entity);
    void SoftDeleteAndRestore(T entity);
    Task SaveChangesAsync();
}