using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IRepository<T, TKey> where T : AuditableEntity<TKey>
    {
        T GetById(TKey id);
        Task<T> GetByIdAsync(TKey id);
        IQueryable<T> GetList();
        Task<IQueryable<T>> GetListAsync();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);
        TKey Add(T entity);
        Task<TKey> AddAsync(T entity);
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        int UpdateRange(T[] entities);
        Task<int> UpdateRangeAsync(T[] entities);
        int Delete(T entity);
        Task<int> DeleteAsync(T entity);
        int DeletePermanently(TKey id);
        Task<int> DeletePermanentlyAsync(TKey id);
        IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes);
        int ExecuteSQL(string sql);
        Task<int> AddRangeAsync(IList<T> entites);
        Task<int> BulkInsertAsyncOld(IList<T> entites);
        Task<int> BulkUpdateAsyncOld(IList<T> entites);
        bool BulkInsert(IList<T> entites);
        bool BulkUpdate(IList<T> entites);
        Task<bool> BulkInsertAsync(IList<T> entites);
        Task<bool> BulkUpdateAsync(IList<T> entites);
    }
}
