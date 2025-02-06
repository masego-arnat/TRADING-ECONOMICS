using System.Linq.Expressions;

namespace OA.Data.Interface
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        ////Task UpdateAsync(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);
        Task UpdateAsync(IEnumerable<T> entity);
        Task DeleteAsync(T entity);
        Task DeleteByIdAsync(int id);
    }
}
