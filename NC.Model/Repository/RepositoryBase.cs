using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Model.EntityModels;

namespace NC.Model.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepository<T, Guid> where T : EntityBase
    {
        protected readonly CTX dbContext;

        protected DbSet<T> DbSet => dbContext.Set<T>();

        protected RepositoryBase(CTX dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            //this.dbContext.EnsureCreated();
        }

        //#region Insert

        //public virtual int Add(T entity)
        //{
        //    dbContext.Add(entity);
        //    return SaveChange();
        //}

        //public virtual async Task<int> AddAsync(T entity)
        //{
        //    dbContext.Add(entity);
        //    return await SaveChangesAsync();
        //}

        //public virtual int AddRange(ICollection<T> entities)
        //{
        //    dbContext.AddRange(entities);
        //    return SaveChange();
        //}

        //public virtual async Task<int> AddRangeAsync(ICollection<T> entities)
        //{
        //    dbContext.AddRange(entities);
        //    return await SaveChangesAsync();
        //}

        ////public virtual void BulkInsert(IList<T> entities, string destinationTableName = null)
        ////{
        ////    dbContext.BulkInsert<T>(entities, destinationTableName);
        ////}

        ////public int AddBySql(string sql)
        ////{
        ////    return dbContext.ExecuteSqlWithNonQuery(sql);
        ////}

        //#endregion

        //#region Update

        ////public int DeleteBySql(string sql)
        ////{
        ////    return dbContext.ExecuteSqlWithNonQuery(sql);
        ////}

        //public virtual int Update(T entity)
        //{
        //    dbContext.Update(entity);
        //    return dbContext.Edit<T>(entity);
        //}

        //public virtual int UpdateRange(ICollection<T> entities)
        //{
        //    return dbContext.EditRange(entities);
        //}
        ///// <summary>
        ///// update query datas by columns.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="where"></param>
        ///// <param name="updateExp"></param>
        ///// <returns></returns>
        //public virtual int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        //{
        //    return dbContext.Update(where, updateExp);
        //}

        //public virtual async Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp)
        //{
        //    return await dbContext.UpdateAsync(@where, updateExp);
        //}
        //public virtual int Update(T model, params string[] updateColumns)
        //{
        //    dbContext.Update(model, updateColumns);
        //    return dbContext.SaveChanges();
        //}

        //public virtual int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        //{
        //    return dbContext.Update(where, updateFactory);
        //}

        //public virtual async Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory)
        //{
        //    return await dbContext.UpdateAsync(where, updateFactory);
        //}

        //public int UpdateBySql(string sql)
        //{
        //    return dbContext.ExecuteSqlWithNonQuery(sql);
        //}

        //#endregion

        //#region Delete

        //public virtual int Delete(TKey key)
        //{
        //    return dbContext.Delete<T>(key);
        //}

        //public virtual int Delete(Expression<Func<T, bool>> @where)
        //{
        //    return dbContext.Delete(where);
        //}

        //public virtual async Task<int> DeleteAsync(Expression<Func<T, bool>> @where)
        //{
        //    return await dbContext.DeleteAsync(where);
        //}


        //#endregion

        //#region Query

        //public virtual int Count(Expression<Func<T, bool>> @where = null)
        //{
        //    return dbContext.Count(where);
        //}

        //public virtual async Task<int> CountAsync(Expression<Func<T, bool>> @where = null)
        //{
        //    return await dbContext.CountAsync(where);
        //}


        //public virtual bool Exist(Expression<Func<T, bool>> @where = null)
        //{
        //    return dbContext.Exist(where);
        //}

        //public virtual async Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null)
        //{
        //    return await dbContext.ExistAsync(where);
        //}

        ///// <summary>
        ///// 根据主键获取实体。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public virtual T GetSingle(TKey key)
        //{
        //    return DbSet.Find(key);
        //}

        //public T GetSingle(TKey key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        //{
        //    if (includeFunc == null) return GetSingle(key);
        //    return includeFunc(DbSet.Where(m => m.Id.Equal(key))).AsNoTracking().FirstOrDefault();
        //}

        ///// <summary>
        ///// 根据主键获取实体。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public virtual async Task<T> GetSingleAsync(TKey key)
        //{
        //    return await dbContext.FindAsync<T>(key);
        //}

        ///// <summary>
        ///// 获取单个实体。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        //public virtual T GetSingleOrDefault(Expression<Func<T, bool>> @where = null)
        //{
        //    return dbContext.GetSingleOrDefault(@where);
        //}

        ///// <summary>
        ///// 获取单个实体。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        //public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null)
        //{
        //    return await dbContext.GetSingleOrDefaultAsync(where);
        //}

        ///// <summary>
        ///// 获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        //public virtual IQueryable<T> Get(Expression<Func<T, bool>> @where = null)
        //{
        //    return (@where != null ? DbSet.Where(@where).AsNoTracking() : DbSet.AsNoTracking());
        //}

        ///// <summary>
        ///// 获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        //public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null)
        //{
        //    return await DbSet.Where(where).ToListAsync();
        //}

        ///// <summary>
        ///// 分页获取实体列表。建议：如需使用Include和ThenInclude请重载此方法。
        ///// </summary>
        //public virtual IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] @orderby)
        //{
        //    var filter = Get(where);
        //    if (orderby != null)
        //    {
        //        foreach (var func in orderby)
        //        {
        //            filter = asc ? filter.OrderBy(func).AsQueryable() : filter.OrderByDescending(func).AsQueryable();
        //        }
        //    }
        //    return filter.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        //}

        //public List<T> GetBySql(string sql)
        //{
        //    return dbContext.SqlQuery<T, T>(sql);
        //}

        //public List<TView> GetViews<TView>(string sql)
        //{
        //    var list = dbContext.SqlQuery<T, TView>(sql);
        //    return list;
        //}

        //public List<TView> GetViews<TView>(string viewName, Func<TView, bool> @where)
        //{
        //    var list = dbContext.SqlQuery<T, TView>($"select * from {viewName}");
        //    if (where != null)
        //    {
        //        return list.Where(where).ToList();
        //    }

        //    return list;
        //}

        //#endregion

        public void Dispose()
        {
            dbContext?.Dispose();
        }

        //public int Delete(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetSingle(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetSingle(Guid key, Func<IQueryable<T>, IQueryable<T>> includeFunc)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<T> GetSingleAsync(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        public int SaveChange()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
