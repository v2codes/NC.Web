using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NC.Core.Entities;

namespace NC.Core.Services
{
    /// <summary>
    /// Service接口
    /// </summary>
    public interface IService<T, TKey> where T : Entity<TKey>
    {
        #region  Insert
        int Add(T entity);
        Task<int> AddAsync(T entity);
        int AddRange(ICollection<T> entities);
        Task<int> AddRangeAsync(ICollection<T> entities);
        //void BulkInsert(IList<T> entities, string destinationTableName = null);
        //int AddBySql(string sql);
        #endregion
    }
}
