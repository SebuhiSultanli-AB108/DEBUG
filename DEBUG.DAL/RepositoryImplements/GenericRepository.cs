using DEBUG.Core.Entities;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DEBUG.DAL.RepositoryImplements;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table = _context.Set<T>();
    public async Task CreateAsync(T entity)
        => await Table.AddAsync(entity);

    public async Task RangeCreateAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            await Table.AddAsync(entity);
    }
    public async Task HardDeleteAsync(T entity)
        => await Task.Run(() => Table.Remove(entity));
    public void SoftDeleteAndRestore(T entity)
    {
        entity.IsDeleted = entity.IsDeleted ? false : true;
        entity.DeletedAt = DateTime.Now;
    }
    public async Task<IEnumerable<T>> GetAllAsync(short pageNo, short take, params string[]? includes)
    {
        var query = Table.AsQueryable().Skip((pageNo - 1) * take).Take(take);
        query = _addIncludes(query, includes);
        return await query.ToListAsync();
    }
    public async Task<IEnumerable<T>> GetWhereAsync(short pageNo, short take, Expression<Func<T, bool>> expression, params string[]? includes)
    {
        var query = Table.AsQueryable().Skip((pageNo - 1) * take).Take(take);
        query = _addIncludes(query, includes);
        query = _addWhere(query, expression);
        return await query.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(int id, Expression<Func<T, bool>>? where = null, params string[]? includes)
    {
        var query = Table.AsQueryable();
        query = _addWhere(query, where);
        query = _addIncludes(query, includes);
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<List<T>?> GetRangeByIdsAsync(int[] ids, Expression<Func<T, bool>>? where = null, params string[]? includes)
    {
        var query = Table.AsQueryable();
        query = _addWhere(query, where);
        query = _addIncludes(query, includes);
        List<T> list = new();
        foreach (var id in ids)
            list.Add(await query.FirstOrDefaultAsync(x => x.Id == id));
        return list;
    }
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
    IQueryable<T> _addWhere(IQueryable<T> query, Expression<Func<T, bool>>? where = null)
    {
        if (where != null)
            query = query.Where(where);
        return query;
    }
    IQueryable<T> _addIncludes(IQueryable<T> query, params string[]? includes)
    {
        if (includes != null && includes.Count() != 0)
            foreach (var include in includes)
                query = query.Include(include);
        return query;
    }
}