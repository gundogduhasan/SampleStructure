global using System;
global using Common.Entites;

using Data.DBContext;
using DataAccess.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using EFCore.BulkExtensions;
using System.IO;

namespace DataAccess.Repository
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : AuditableEntity<TKey>
    {
        protected SampleContext _bogforingContext;

        public Repository()
        {
            IServiceCollection services = new ServiceCollection();
            services.InitializeDatabase();

            ServiceProvider provider = services.BuildServiceProvider();

            _bogforingContext = provider.GetService<SampleContext>();

            _bogforingContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public T GetById(TKey id)
        {
            return GetDBSet().Find(id);
        }
        public async Task<T> GetByIdAsync(TKey id)
        {
            return await GetDBSet().FindAsync(id);
        }
        public IQueryable<T> GetList()
        {
            return GetListAsQueryable();
        }
        public async Task<IQueryable<T>> GetListAsync()
        {
            return await Task.FromResult(GetListAsQueryable());
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return GetListAsQueryable().Where(predicate);
        }
        public async Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(GetListAsQueryable().Where(predicate));
        }
        public TKey Add(T entity)
        {
            _bogforingContext.Entry(entity).State = EntityState.Added;

            _bogforingContext.Add(entity);
            //entity.CreatedDate = DateTime.Now;

            _bogforingContext.SaveChanges();

            return entity.Id;
            // return (TKey)GetPropValue(entity, "Id");
        }
        public async Task<TKey> AddAsync(T entity)
        {
            _bogforingContext.Entry(entity).State = EntityState.Added;

            await _bogforingContext.AddAsync(entity);
            // entity.CreatedDate = DateTime.Now;

            await _bogforingContext.SaveChangesAsync();

            return entity.Id;
            //return (TKey)GetPropValue(entity, "Id");
        }
        public int Update(T entity)
        {
            _bogforingContext.Entry(entity).State = EntityState.Modified;
            _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);

            _bogforingContext.Update(entity);

            return _bogforingContext.SaveChanges();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);
            _bogforingContext.Entry(entity).State = EntityState.Modified;

            _bogforingContext.Update(entity);

            return await _bogforingContext.SaveChangesAsync();
        }
        public int UpdateRange(T[] entities)
        {
            foreach (T entity in entities)
            {
                _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
                _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);
            }

            _bogforingContext.UpdateRange(entities);

            return _bogforingContext.SaveChanges();
        }
        public async Task<int> UpdateRangeAsync(T[] entities)
        {
            foreach (T entity in entities)
            {
                _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
                _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);
            }

            _bogforingContext.UpdateRange(entities);

            return await _bogforingContext.SaveChangesAsync();
        }
        /// <summary>
        /// Update given entity 'IsDeleted' prop to true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(T entity)
        {
            entity.IsDeleted = true;

            _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
            _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);

            _bogforingContext.Update(entity);

            return _bogforingContext.SaveChanges();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            entity.IsDeleted = true;

            _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
            _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);

            _bogforingContext.Update(entity);

            return await _bogforingContext.SaveChangesAsync();
        }
        /// <summary>
        /// Delete permanently entity found by given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePermanently(TKey id)
        {
            T entity = GetById(id);

            _bogforingContext.Entry<T>(entity).State = EntityState.Deleted;

            _bogforingContext.Remove(entity);

            return _bogforingContext.SaveChanges();
        }
        /// <summary>
        /// Delete permanently entity found by given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeletePermanentlyAsync(TKey id)
        {
            T entity = GetById(id);

            _bogforingContext.Entry<T>(entity).State = EntityState.Deleted;

            _bogforingContext.Remove(entity);

            return await _bogforingContext.SaveChangesAsync();
        }
        public IQueryable<T> IncludeMany(params Expression<Func<T, object>>[] includes)
        {
            return GetDBSet().IncludeMultiple(includes);
        }
        public int ExecuteSQL(string sql)
        {
            return _bogforingContext.Database.ExecuteSqlRaw(sql);
        }
        private IQueryable<T> GetListAsQueryable()
        {
            return GetDBSet().AsQueryable();
        }
        private DbSet<T> GetDBSet()
        {
            return _bogforingContext.Set<T>();
        }
        public async Task<int> AddRangeAsync(IList<T> entities)
        {
            foreach (T entity in entities)
                _bogforingContext.Entry<T>(entity).State = EntityState.Added;


            await _bogforingContext.AddRangeAsync(entities);

            return await _bogforingContext.SaveChangesAsync();
        }
        public async Task<int> BulkInsertAsyncOld(IList<T> entities)
        {
            foreach (T entity in entities)
                _bogforingContext.Entry<T>(entity).State = EntityState.Added;


            return await _bogforingContext.SaveChangesAsync();
        }
        public async Task<int> BulkUpdateAsyncOld(IList<T> entities)
        {
            foreach (T entity in entities)
            {
                _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
                _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);
            }

            return await _bogforingContext.SaveChangesAsync();
        }

        // BULK Section
        public bool BulkInsert(IList<T> entities)
        {
            try
            {
                foreach (T entity in entities)
                    _bogforingContext.Entry<T>(entity).State = EntityState.Added;

               // _bogforingContext.BulkInsert(entities);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public bool BulkUpdate(IList<T> entities)
        {
            try
            {
                foreach (T entity in entities)
                {
                    _bogforingContext.Entry<T>(entity).State = EntityState.Modified;
                    _bogforingContext.Entry(entity).CurrentValues.SetValues(entity);
                }
                //_bogforingContext.BulkUpdate(entities);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> BulkInsertAsync(IList<T> entities)
        {
            try
            {
                foreach (T entity in entities)
                    _bogforingContext.Entry<T>(entity).State = EntityState.Added;

                string date = DateTime.Now.ToShortDateString();
                string time = DateTime.Now.ToShortTimeString();
                var path = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamWriter streamWriter = new StreamWriter(path + $"log_" + date + ".txt", true))
                {
                    await streamWriter.WriteLineAsync($"Log Level : Information | Event ID : 0 | Event Time : {time} | Message : BulkInsert has been started with " + entities.Count + " entities.");

                    streamWriter.Close();
                    await streamWriter.DisposeAsync();
                }

               // await _bogforingContext.BulkInsertAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                string date = DateTime.Now.ToShortDateString();
                string time = DateTime.Now.ToShortTimeString();
                var path = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamWriter streamWriter = new StreamWriter(path + $"log_" + date + ".txt", true))
                {
                    await streamWriter.WriteLineAsync($"Log Level : Information | Event ID : 1 | Event Time : {time} | Message : " + ex.Message);

                    streamWriter.Close();
                    await streamWriter.DisposeAsync();
                }
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> BulkUpdateAsync(IList<T> entities)
        {
            try
            {
                // Aşağıdaki hayatı almamak için gelen entry listesini DISTINCT yapıyoruz.

                // Microsoft.Data.SqlClient.SqlException: 'The MERGE statement attempted to UPDATE or DELETE the same row more than once.' \\
                // This happens when a target row matches more than one source row. A MERGE statement cannot UPDATE/DELETE the same row of \\
                // the target table multiple times. Refine the ON clause to ensure a target row matches at most one source row, or use the \\
                // GROUP BY clause to group the source rows.                                                                               \\

                string date = DateTime.Now.ToShortDateString();
                string time = DateTime.Now.ToShortTimeString();
                var path = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamWriter streamWriter = new StreamWriter(path + $"log_" + date + ".txt", true))
                {
                    await streamWriter.WriteLineAsync($"Log Level : Information | Event ID : 0 | Event Time : {time} | Message : BulkUpdate has been started with " + entities.Count + " entities.");

                    streamWriter.Close();
                    await streamWriter.DisposeAsync();
                }

              //  entities = entities.DistinctBy(x => x.Id).ToList();
               // await _bogforingContext.BulkUpdateAsync(entities);

                return true;
            }
            catch (Exception ex)
            {
                string date = DateTime.Now.ToShortDateString();
                string time = DateTime.Now.ToShortTimeString();
                var path = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamWriter streamWriter = new StreamWriter(path + $"log_" + date + ".txt", true))
                {
                    await streamWriter.WriteLineAsync($"Log Level : Information | Event ID : 1 | Event Time : {time} | Message : " + ex.Message);

                    streamWriter.Close();
                    await streamWriter.DisposeAsync();
                }
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
