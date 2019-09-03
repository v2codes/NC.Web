using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NC.Core.Entities;
using NC.Core.Repositories;
using NC.Web.Common.Extensions;

namespace NC.Core.Services
{
    /// <summary>
    /// Service 抽象类
    /// </summary>
    public abstract class BaseService<T, TKey> : IService<T, TKey> where T : class, IEntity<TKey>
    {
        /// <summary>
        /// 仓储服务
        /// </summary>
        protected readonly IRepository<T, TKey> _repo;
        public BaseService(IRepository<T, TKey> repository)
        {
            _repo = repository;
        }

        #region Insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            return _repo.Add(entity);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            return await _repo.AddAsync(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int AddRange(ICollection<T> entities)
        {
            return _repo.AddRange(entities);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAsync(ICollection<T> entities)
        {
            return await _repo.AddRangeAsync(entities);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="destinationTableName"></param>
        public void BatchInsert(IList<T> entities, string destinationTableName = null)
        {
            _repo.BatchInsert(entities, destinationTableName);
        }
        #endregion

        #region Update

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            return _repo.Update(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            return await _repo.UpdateAsync(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public int Update(T model, params string[] updateColumns)
        {
            return _repo.Update(model, updateColumns);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T model, params string[] updateColumns)
        {
            return await _repo.UpdateAsync(model, updateColumns);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return _repo.Update(@where, updateFactory);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return await _repo.UpdateAsync(@where, updateFactory);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int BatchUpdate(ICollection<T> entities)
        {
            return _repo.BatchUpdate(entities);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateAsync(ICollection<T> entities)
        {
            return await _repo.BatchUpdateAsync(entities);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            return _repo.BatchUpdate(@where, updateExp);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            return await _repo.BatchUpdateAsync(@where, updateExp);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(TKey key)
        {
            return _repo.Delete(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TKey key)
        {
            return await _repo.DeleteAsync(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> @where)
        {
            return _repo.Delete(@where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> @where)
        {
            return await _repo.DeleteAsync(@where);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int LogicDelete(TKey key)
        {
            return _repo.LogicDelete(key);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> LogicDeleteAsync(TKey key)
        {
            return await _repo.LogicDeleteAsync(key);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int LogicDelete(Expression<Func<T, bool>> @where)
        {
            return _repo.LogicDelete(@where);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> LogicDeleteAsync(Expression<Func<T, bool>> @where)
        {
            return await _repo.LogicDeleteAsync(@where);
        }
        #endregion

        #region Query
        /// <summary>
        /// 数据条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>

        public int Count(Expression<Func<T, bool>> @where = null)
        {
            return _repo.Count(@where);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        {
            return await _repo.CountAsync(where);
        }

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> @where = null)
        {
            return _repo.Any(where);
        }

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> @where = null)
        {
            return await _repo.AnyAsync(where);
        }

        /// <summary>
        /// 根据 Id 获取单个实体
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Find(TKey key)
        {
            return _repo.Find(key);
        }

        /// <summary>
        /// 根据 Id 获取单个实体
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> FindAsync(TKey key)
        {
            return await _repo.FindAsync(key);
        }

        /// <summary>
        /// 获取第一条记录
        /// 注：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> @where = null)
        {
            return _repo.FirstOrDefault(where);
        }

        /// <summary>
        /// 获取第一条记录
        /// 注：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        {
            return await _repo.FirstOrDefaultAsync(where);
        }

        /// <summary>
        /// 查询
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> Query(Expression<Func<T, bool>> @where = null)
        {
            return _repo.Query(where);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> @where = null)
        {
            return await _repo.QueryAsync(where);
        }

        /// <summary>
        /// 分页查询
        /// 建议：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public PagedList<T> QueryPagedList(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        {
            return _repo.QueryPagedList(where, pageSize, pageIndex, asc, orderby);
        }
        #endregion
    }
}
