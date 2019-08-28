using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Core.Database;
using NC.Core.Entities;

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
        /// DbContext
        /// </summary>
        private readonly CTX _ctx;
        /// <summary>
        /// DbSet
        /// </summary>
        protected DbSet<T> DbSet => _ctx.GetDbSet<T>();
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ctx"></param>
        public BaseRepository(CTX ctx)
        {
            _ctx = ctx;
        }

        #region Insert
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(T entity)
        {
            _ctx.Add(entity);
            return SaveChange();
        }
        /// <summary>
        /// async insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> AddAsync(T entity)
        {
            _ctx.Add(entity);
            return SaveChangeAsync();
        }
        /// <summary>
        /// batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int AddRange(ICollection<T> entities)
        {
            _ctx.AddRange(entities);
            return SaveChange();
        }
        /// <summary>
        /// async batch insert
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<int> AddRangeAsync(ICollection<T> entities)
        {
            _ctx.AddRange(entities);
            return SaveChangeAsync();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int SaveChange()
        {
            return _ctx.SaveChanges();
        }
        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangeAsync()
        {
            return _ctx.SaveChangesAsync();
        }
        #endregion

        #region Dispose
        /// <summary>
        /// dispose
        /// </summary>
        public void Dispose()
        {
        }
        #endregion
    }
}
