using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NC.Core.Entities;
using NC.Core.Repositories;

namespace NC.Core.Services
{
    /// <summary>
    /// Service 基类
    /// </summary>
    public abstract class BaseService<T, TKey> : IService<T, TKey> where T : Entity<TKey>
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
        /// 异步新增
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
        /// 异步批量新增
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<int> AddRangeAsync(ICollection<T> entities)
        {
            return _repository.AddRangeAsync(entities);
        }
    }
}
