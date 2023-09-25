using System.Linq.Expressions;
using DistantSchool.Helpers;

namespace DistantSchool.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<Result<bool>> Add(T entity);
    Task<Result<bool>> Delete(T entity);
    Task<Result<bool>> Update(T entity);
    Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null);
}