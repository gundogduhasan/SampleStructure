global using System;
global using System.Collections.Generic;
global using Common.Entites;
global using System.Linq;
global using System.Threading.Tasks;

using DataAccess.Repository;
using System.Linq.Expressions;

namespace Business.EntityServices
{
    public class BaseService<T> : BaseService<T, Guid> where T : AuditableEntity<Guid>
    { }

    public class BaseService<T, TKey> : IServiceManager<T, TKey> where T : AuditableEntity<TKey>
    {
        protected readonly IRepository<T, TKey> repository;
        protected BaseService()
        {
            repository = new Repository<T, TKey>();
        }

        public T GetById(TKey id)
        {
            return repository.GetById(id);
        }
        public async Task<T> GetByIdAsync(TKey id)
        {
            return await repository.GetByIdAsync(id);
        }
        public IQueryable<T> GetList()
        {
            return repository.GetList();
        }
        public async Task<IQueryable<T>> GetListAsync()
        {
            return await repository.GetListAsync();
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return repository.GetWhere(predicate);
        }
        public async Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.GetWhereAsync(predicate);
        }
        public TKey Add(T entity)
        {
            return repository.Add(entity);
        }
        public async Task<TKey> AddAsync(T entity)
        {
            return await repository.AddAsync(entity);
        }
        public int Update(T entity)
        {
            return repository.Update(entity);
        }
        public async Task<int> UpdateAsync(T entity)
        {
            return await repository.UpdateAsync(entity);
        }
        public int UpdateRange(T[] entities)
        {
            return repository.UpdateRange(entities);
        }
        public async Task<int> UpdateRangeAsync(T[] entities)
        {
            return await repository.UpdateRangeAsync(entities);
        }
        public int Delete(T entity)
        {
            return repository.Delete(entity);
        }
        public async Task<int> DeleteAsync(T entity)
        {
            return await repository.DeleteAsync(entity);
        }
        public int DeletePermanently(TKey id)
        {
            return repository.DeletePermanently(id);
        }
        public async Task<int> DeletePermanentlyAsync(TKey id)
        {
            return await repository.DeletePermanentlyAsync(id);
        }
        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return repository.IncludeMany(includes);
        }
        public int ExecuteSQL(string sql)
        {
            return repository.ExecuteSQL(sql);
        }
        public async Task<int> AddRangeAsync(IList<T> entites)
        {
            return await repository.AddRangeAsync(entites);
        }
        public async Task<int> BulkInsertAsyncOld(IList<T> entites)
        {
            return await repository.BulkInsertAsyncOld(entites);
        }
        public async Task<int> BulkUpdateAsyncOld(IList<T> entites)
        {
            return await repository.BulkUpdateAsyncOld(entites);
        }
        public bool BulkInsert(IList<T> entites)
        {
            return repository.BulkInsert(entites);
        }
        public bool BulkUpdate(IList<T> entites)
        {
            return repository.BulkUpdate(entites);
        }
        public async Task<bool> BulkInsertAsync(IList<T> entites)
        {
            return await repository.BulkInsertAsync(entites);
        }
        public async Task<bool> BulkUpdateAsync(IList<T> entites)
        {
            return await repository.BulkUpdateAsync(entites);
        }
    }
}
