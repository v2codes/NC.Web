using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Core.Database;
using NC.Core.Entities;
using Z.EntityFramework.Plus;

namespace NC.Core.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseRepository<T, TKey> : IRepository<T, TKey> where T : class, IEntity<TKey>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly DbContext _db;
        /// <summary>
        /// DbSet
        /// </summary>
        protected DbSet<T> DbSet => _db.Set<T>();
        public BaseRepository(DbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(_db));
        }

        #region Insert
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
        {
            _db.Add(entity);
            return SaveChange();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return await SaveChangeAsync();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int AddRange(ICollection<T> entities)
        {
            _db.AddRange(entities);
            return SaveChange();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAsync(ICollection<T> entities)
        {
            await _db.AddRangeAsync(entities);
            return await SaveChangeAsync();
        }
        /// <summary>
        /// 批量新增
        /// TODO：暂未实现（bulk insert by sqlbulkcopy, and with transaction.）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="destinationTableName"></param>
        public void BatchInsert(IList<T> entities, string destinationTableName = null)
        {
            if (!_db.Database.IsSqlServer()) // && !_db.Database.IsMySql()
                throw new NotSupportedException("This method only supports for SQL Server."); //  or MySql
            throw new NotImplementedException("该方法暂未实现。");
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Add(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
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
            _db.Update(entity);
            return SaveChange();
        }
        /// <summary>
        /// 更新
        /// TODO：暂未实现
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException("该方法暂未实现。");
            _db.Update(entity);
            return await SaveChangeAsync();
        }

        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public int Update(T model, params string[] updateColumns)
        {
            if (updateColumns != null && updateColumns.Length > 0)
            {
                if (_db.Entry(model).State == EntityState.Added || _db.Entry(model).State == EntityState.Detached)
                {
                    DbSet.Attach(model);
                }
                foreach (var propertyName in updateColumns)
                {
                    _db.Entry(model).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                _db.Entry(model).State = EntityState.Modified;
            }
            return SaveChange();
        }

        /// <summary>
        /// 更新
        /// TODO：暂未实现
        /// </summary>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T model, params string[] updateColumns)
        {
            throw new NotImplementedException("该方法暂未实现。");
            if (updateColumns != null && updateColumns.Length > 0)
            {
                if (_db.Entry(model).State == EntityState.Added || _db.Entry(model).State == EntityState.Detached)
                {
                    DbSet.Attach(model);
                }
                foreach (var propertyName in updateColumns)
                {
                    _db.Entry(model).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                _db.Entry(model).State = EntityState.Modified;
            }
            return await SaveChangeAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return DbSet.Where(where).Update(updateFactory);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        {
            DbSet.Where(where).Update(updateFactory);
            return await _db.Set<T>().Where(where).UpdateAsync(updateFactory);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int BatchUpdate(ICollection<T> entities)
        {
            _db.UpdateRange(entities);
            return SaveChange();
        }

        /// <summary>
        /// 批量更新
        /// TODO：暂未实现
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateAsync(ICollection<T> entities)
        {
            throw new NotImplementedException("该方法暂未实现。");
            _db.UpdateRange(entities);
            return await SaveChangeAsync();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public int BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            return DbSet.Where(where).Update(updateExp);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        {
            DbSet.Where(where).Update(updateExp);
            return await _db.Set<T>().Where(where).UpdateAsync(updateExp);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Update(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region Delete

        /// <summary>
        /// 删除
        /// TODO：该方法待验证，动态创建泛型实例并采用 Entry 方式删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Delete(TKey key)
        {
            //var entity = Find(key);
            //_db.Remove(entity);
            var instance = Activator.CreateInstance<T>();
            instance.Id = key;
            var entry = _db.Entry(instance);
            entry.State = EntityState.Deleted;
            return _db.SaveChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TKey key)
        {
            //var entity = Find(key);
            //_db.Remove(entity);
            var instance = Activator.CreateInstance<T>();
            instance.Id = key;
            var entry = _db.Entry(instance);
            entry.State = EntityState.Deleted;
            return await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        {
            return await DbSet.Where(where).DeleteAsync();
        }

        /// <summary>
        /// 逻辑删除
        /// TODO：逻辑删除时 Status 应赋值枚举，暂时硬编码 999
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int LogicDelete(TKey key)
        {
            var instance = Activator.CreateInstance<T>();
            instance.Id = key;
            instance.Status = 999;
            var entry = _db.Entry(instance);
            entry.Property(p => p.Status).IsModified = true;
            entry.State = EntityState.Modified;
            return _db.SaveChanges();
        }

        /// <summary>
        /// 逻辑删除
        /// TODO：逻辑删除时 Status 应赋值枚举，暂时硬编码 999
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<int> LogicDeleteAsync(TKey key)
        {
            var instance = Activator.CreateInstance<T>();
            instance.Id = key;
            instance.Status = 999;
            var entry = _db.Entry(instance);
            entry.Property(p => p.Status).IsModified = true;
            entry.State = EntityState.Modified;
            return await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 逻辑删除
        /// TODO：逻辑删除时 Status 应赋值枚举，暂时硬编码 999
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public int LogicDelete(Expression<Func<T, bool>> where)
        {
            // TODO 
            return DbSet.Where(where).Update(x=>);
        }

        public Task<int> LogicDeleteAsync(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Delete(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region Query

        #endregion

        public int Count(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public bool Any(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Find(TKey key)
        {
            throw new NotImplementedException();
        }

        public T Find(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            throw new NotImplementedException();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        public List<T> Query(string sql)
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

        public List<TView> QueryViews<TView>(string sql)
        {
            throw new NotImplementedException();
        }

        public List<TView> QueryViews<TView>(string viewName, Func<TView, bool> where)
        {
            throw new NotImplementedException();
        }

        public int SaveChange()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            throw new NotImplementedException();
        }


    }
}
