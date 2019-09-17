using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Core.Database;
using NC.Core.Entities;
using NC.Common.Extensions;
using Z.EntityFramework.Plus;

namespace NC.Core.Repositories
{
    /// <summary>
    /// 仓储抽象类
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
        public virtual int Add(T entity)
        {
            _db.Add(entity);
            return SaveChange();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            return await SaveChangeAsync();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int AddRange(ICollection<T> entities)
        {
            _db.AddRange(entities);
            return SaveChange();
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> AddRangeAsync(ICollection<T> entities)
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
        public virtual void BulkInsert(IList<T> entities, string destinationTableName = null)
        {
            if (!_db.Database.IsSqlServer()) // && !_db.Database.IsMySql()
                throw new NotSupportedException("This method only supports for SQL Server."); //  or MySql
            using (SqlConnection connect = new SqlConnection())
            {
                connect.ConnectionString = _db.Database.GetDbConnection().ConnectionString;
                if (connect.State != System.Data.ConnectionState.Open)
                {
                    connect.Open();
                }
                string tableName = string.Empty;
                var tType = typeof(T);
                var tableAttribute = tType.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
                if (tableAttribute != null)
                {
                    tableName = ((TableAttribute)tableAttribute).Name;
                }
                else
                {
                    tableName = tType.Name;
                }
                using (var transaction = connect.BeginTransaction())
                {
                    try
                    {
                        var bulkCopy = new SqlBulkCopy(connect, SqlBulkCopyOptions.Default, transaction)
                        {
                            BatchSize = entities.Count,
                            DestinationTableName = tableName
                        };
                        GenerateColumnMappings<T>(bulkCopy.ColumnMappings);
                        bulkCopy.WriteToServer(entities.ToDataTable());
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int Add(string sql)
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
        public virtual int Update(T entity)
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
        public virtual async Task<int> UpdateAsync(T entity)
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
        public virtual int Update(T model, params string[] updateColumns)
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
        public virtual async Task<int> UpdateAsync(T model, params string[] updateColumns)
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
            return await SaveChangeAsync();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public virtual int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            return DbSet.Where(where).Update(updateFactory);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateFactory"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        {
            DbSet.Where(where).Update(updateFactory);
            return await _db.Set<T>().Where(where).UpdateAsync(updateFactory);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int BatchUpdate(ICollection<T> entities)
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
        public virtual async Task<int> BatchUpdateAsync(ICollection<T> entities)
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
        public virtual int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            return DbSet.Where(where).Update(updateExp);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where"></param>
        /// <param name="updateExp"></param>
        /// <returns></returns>
        public virtual async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        {
            DbSet.Where(where).Update(updateExp);
            return await _db.Set<T>().Where(where).UpdateAsync(updateExp);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int Update(string sql)
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
        public virtual int Delete(TKey key)
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
        public virtual async Task<int> DeleteAsync(TKey key)
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
        public virtual int Delete(Expression<Func<T, bool>> @where)
        {
            return DbSet.Where(where).Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> @where)
        {
            return await DbSet.Where(where).DeleteAsync();
        }

        /// <summary>
        /// 逻辑删除
        /// TODO：逻辑删除时 Status 应赋值枚举，暂时硬编码 999
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual int LogicDelete(TKey key)
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
        public virtual async Task<int> LogicDeleteAsync(TKey key)
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
        public virtual int LogicDelete(Expression<Func<T, bool>> @where)
        {
            var instance = Activator.CreateInstance<T>();
            instance.Status = 999;
            return DbSet.Where(where).Update(x => instance);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual Task<int> LogicDeleteAsync(Expression<Func<T, bool>> @where)
        {
            var instance = Activator.CreateInstance<T>();
            instance.Status = 999;
            return DbSet.Where(where).UpdateAsync(x => instance);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual int Delete(string sql)
        {
            return _db.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region Query
        /// <summary>
        /// 数据条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? DbSet.Count() : DbSet.Count(where);
        }

        /// <summary>
        /// 数据条数
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        {
            return await (where == null ? DbSet.CountAsync() : DbSet.CountAsync(where));
        }

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Any(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? DbSet.Any() : DbSet.Any(where);
        }

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> @where = null)
        {
            return await (where == null ? DbSet.AnyAsync() : DbSet.AnyAsync(where));
        }

        /// <summary>
        /// 根据 Id 获取单个实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T Find(TKey key)
        {
            return DbSet.Find(key);
        }

        /// <summary>
        /// 根据 Id 获取单个实体
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(TKey key)
        {
            return await DbSet.FindAsync(key);
        }

        /// <summary>
        /// 根据Id获取数据
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeFunc"></param>
        /// <returns></returns>
        public virtual T Find(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            if (includeFunc == null)
            {
                return Find(key);
            }
            return includeFunc(DbSet.Where(m => m.Id.Equals(key))).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// 根据Id获取数据
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="key"></param>
        /// <param name="includeFunc"></param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        {
            if (includeFunc == null)
            {
                return await FindAsync(key);
            }
            return await includeFunc(DbSet.Where(m => m.Id.Equals(key))).AsNoTracking().FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取第一条记录
        /// 注：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T FirstOrDefault(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? DbSet.FirstOrDefault() : DbSet.FirstOrDefault(where);
        }

        /// <summary>
        /// 获取第一条记录
        /// 注：如需使用Include和ThenInclude请重载此方法。
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        {
            return await (where == null ? DbSet.FirstOrDefaultAsync() : DbSet.FirstOrDefaultAsync(where));
        }

        /// <summary>
        /// 查询
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> @where = null)
        {
            return where == null ? DbSet.AsNoTracking() : DbSet.Where(where).AsNoTracking();
        }

        /// <summary>
        /// 查询
        /// 注：AsNoTracking() 无跟踪查询，也就是说查询出来的对象不能直接做修改。所以，我们在做数据集合查询显示，而又不需要对集合修改并更新到数据库的时候，一定不要忘记加上AsNoTracking
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> QueryAsync(Expression<Func<T, bool>> @where = null)
        {
            return await (where == null ? DbSet.ToListAsync() : DbSet.Where(where).ToListAsync());
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
        public virtual PagedList<T> QueryPagedList(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        {
            var queryable = where == null ? DbSet : DbSet.Where(where);
            if (orderby == null)
            {
                foreach (var func in orderby)
                {
                    queryable = asc ? queryable.OrderBy(func).AsQueryable() : queryable.OrderByDescending(func).AsQueryable();
                }
            }
            return queryable.ToPagedList(pageIndex, pageSize);
        }

        /// <summary>
        /// 查询并转换为指定类型集合
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual List<TView> QueryViews<TView>(string sql)
        {
            return DbSet.FromSql(sql).Cast<TView>().ToList();
        }

        /// <summary>
        /// 查询并转换为指定类型集合
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="viewName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<TView> QueryViews<TView>(string viewName, Func<TView, bool> @where)
        {
            var queryable = DbSet.FromSql($"select * from {viewName}").Cast<TView>();
            if (where != null)
            {
                return queryable.Where(where).ToList();
            }
            return queryable.ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual List<T> Query(string sql)
        {
            return DbSet.FromSql(sql).Cast<T>().ToList();
        }
        #endregion

        #region Helper

        /// <summary>
        /// 实体字段映射数据库列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mappings"></param>
        private void GenerateColumnMappings<TBulk>(SqlBulkCopyColumnMappingCollection mappings) where TBulk : class
        {
            var properties = typeof(TBulk).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetCustomAttributes(typeof(KeyAttribute), true).Any())
                {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, typeof(T).Name + property.Name));
                }
                else
                {
                    mappings.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                }
            }
        }

        #endregion

        #region SaveChange & Dispose
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChange()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual Task<int> SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
        #endregion
    }
}
