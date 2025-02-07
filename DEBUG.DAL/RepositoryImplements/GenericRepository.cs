using DEBUG.Core.Models;
using DEBUG.Core.RepositoryInstances;
using DEBUG.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DEBUG.DAL.RepositoryImplements;

public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table = _context.Set<T>();
    public async Task CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.Now;
        await Table.AddAsync(entity);
    }
    public async Task HardDeleteAsync(T entity)
        => await Task.Run(() => Table.Remove(entity));
    public void SoftDeleteAndRestore(T entity)
    {
        entity.IsDeleted = entity.IsDeleted ? false : true;
        entity.DeletedAt = DateTime.Now;
    }
    public IQueryable<T> GetAll(params string[]? includes)
    {
        var query = Table.AsQueryable();

        if (includes != null && includes.Count() != 0)
            foreach (var include in includes)
                query = query.Include(include);
        return query;
    }
    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, params string[]? includes)
    {
        var query = Table.AsQueryable();

        if (includes != null && includes.Count() != 0)
            foreach (var include in includes)
                query = query.Include(include);
        return query.Where(expression);
    }
    public async Task<T?> GetByIdAsync(int id, params string[]? includes)
    {
        var query = Table.AsQueryable();

        if (includes != null && includes.Count() != 0)
            foreach (var include in includes)
                query = query.Include(include);
        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();


}