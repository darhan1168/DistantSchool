using System.Linq.Expressions;
using DistantSchool.Helpers;
using DistantSchool.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DistantSchool.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly DistantSchool.DataBase.AppContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(DistantSchool.DataBase.AppContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<T> GetById(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        return entity;
    }

    public async Task<Result<bool>> Add(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            
            await _context.SaveChangesAsync();
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            return new Result<bool>(false, $"Failed to add the entity in data base. Error: {e.Message}");
        }
    }

    public async Task<Result<bool>> Delete(T entity)
    {
        try
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            
            _dbSet.Remove(entity);
            
            await _context.SaveChangesAsync();
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            return new Result<bool>(false, $"Failed to delete the entity in data base. Error: {e.Message}");
        }
    }

    public async Task<Result<bool>> Update(T entity)
    {
        try
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            
            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            return new Result<bool>(false, $"Failed to update the entity in data base. Error: {e.Message}");
        }
    }

    public async Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return await orderBy.Compile()(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }
}