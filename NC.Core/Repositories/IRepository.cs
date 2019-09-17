using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NC.Core.Entities;
using NC.Common.Extensions;

namespace NC.Core.Repositories
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepository<T, TKey> : IDisposable where T : IEntity<TKey>
    {
        #region  Insert
        int Add(T entity);
        Task<int> AddAsync(T entity);
        int AddRange(ICollection<T> entities);
        Task<int> AddRangeAsync(ICollection<T> entities);
        void BulkInsert(IList<T> entities, string destinationTableName = null);
        int Add(string sql);
        #endregion

        #region Update
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        int Update(T model, params string[] updateColumns);
        Task<int> UpdateAsync(T model, params string[] updateColumns);
        int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);
        Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);
        int BatchUpdate(ICollection<T> entities);
        Task<int> BatchUpdateAsync(ICollection<T> entities);
        int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);
        Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);
        int Update(string sql);
        #endregion

        #region Delete
        int Delete(TKey key);
        Task<int> DeleteAsync(TKey key);
        int Delete(Expression<Func<T, bool>> @where);
        Task<int> DeleteAsync(Expression<Func<T, bool>> @where);
        int LogicDelete(TKey key);
        Task<int> LogicDeleteAsync(TKey key);
        int LogicDelete(Expression<Func<T, bool>> @where);
        Task<int> LogicDeleteAsync(Expression<Func<T, bool>> @where);
        int Delete(string sql);
        #endregion

        #region Query
        int Count(Expression<Func<T, bool>> @where = null);
        Task<int> CountAsync(Expression<Func<T, bool>> @where = null);
        bool Any(Expression<Func<T, bool>> @where = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> @where = null);
        T Find(TKey key);
        Task<T> FindAsync(TKey key);
        T Find(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc);
        Task<T> FindAsync(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc);
        T FirstOrDefault(Expression<Func<T, bool>> @where = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where = null);
        IQueryable<T> Query(Expression<Func<T, bool>> @where = null);
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> @where = null);
        PagedList<T> QueryPagedList(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] @orderby);
        List<T> Query(string sql);
        List<TView> QueryViews<TView>(string sql);
        List<TView> QueryViews<TView>(string viewName, Func<TView, bool> where);
        #endregion

        #region Save
        int SaveChange();
        Task<int> SaveChangeAsync();
        #endregion

    }
}
