using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NC.Core.Entities;
using NC.Core.Repositories;

namespace NC.Core.Services
{
    /// <summary>
    /// Service 基类
    /// </summary>
    public abstract class BaseService<T, TKey> : IService<T, TKey> where T : class,IEntity<TKey>
    {
        /// <summary>
        /// 仓储服务
        /// </summary>
        protected readonly IRepository<T, TKey> _repository;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repository"></param>
        public BaseService(IRepository<T, TKey> repository)
        {
            _repository = repository;
        }

        #region  Insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            return _repository.Add(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int AddRange(ICollection<T> entities)
        {
            return _repository.AddRange(entities);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<int> AddRangeAsync(ICollection<T> entities)
        {
            return _repository.AddRangeAsync(entities);
        }

        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> where = null)
        {
            return _repository.
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public void BatchInsert(IList<T> entities)
        {
            throw new NotImplementedException();
        }
        #endregion

        public int BatchUpdate(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public int BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchUpdateAsync(ICollection<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> BatchUpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public int Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T Find(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public int LogicDelete(TKey key)
        {
            throw new NotImplementedException();
        }

        public int LogicDelete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<int> LogicDeleteAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<int> LogicDeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> QueryAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> QueryPagedList(Expression<Func<T, bool>> where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T model, params string[] updateColumns)
        {
            throw new NotImplementedException();
        }

        public int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(T model, params string[] updateColumns)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        {
            throw new NotImplementedException();
        }
    }
}
