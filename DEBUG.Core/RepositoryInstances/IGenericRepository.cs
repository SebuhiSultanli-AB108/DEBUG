using DEBUG.Core.Entities;
using System.Linq.Expressions;

namespace DEBUG.Core.RepositoryInstances;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<IEnumerable<T>> GetAllAsync(short pageNo, short take, params string[]? includes);
    Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? where = null, params string[]? includes);
    Task<List<T>?> GetRangeByIdsAsync(int[] ids, Expression<Func<T, bool>>? where = null, params string[]? includes);
    Task<IEnumerable<T>> GetWhereAsync(short pageNo, short take, Expression<Func<T, bool>> expression, params string[]? includes);
    Task CreateAsync(T entity);
    Task RangeCreateAsync(IEnumerable<T> entities);
    Task HardDeleteAsync(T entity);
    void SoftDeleteAndRestore(T entity);
    Task SaveChangesAsync();
}