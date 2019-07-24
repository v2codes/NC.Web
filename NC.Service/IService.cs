using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NC.Service
{
    public interface IService<T> where T : EntityBase
    {
        #region Insert

        int Add(T model);
        Task<int> AddAsync(T entity);
        int AddRange(IList<T> models);
        Task<int> AddRangeAsync(IList<T> entities);
        //void BulkInsert(IList<T> entities);

        #endregion

        //#region Update

        //int Edit(T entity);
        //int EditRange(ICollection<T> entities);
        //int BatchUpdate(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);
        //Task<int> BatchUpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateExp);
        //int Update(T model, params string[] updateColumns);
        //int Update(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);
        //Task<int> UpdateAsync(Expression<Func<T, bool>> @where, Expression<Func<T, T>> updateFactory);

        //#endregion

        //#region Delete

        //int Delete(Guid key);
        //int Delete(Expression<Func<T, bool>> @where);
        //Task<int> DeleteAsync(Expression<Func<T, bool>> @where);

        //#endregion

        //#region Query

        //int Count(Expression<Func<T, bool>> @where = null);
        //Task<int> CountAsync(Expression<Func<T, bool>> @where = null);
        //bool Exist(Expression<Func<T, bool>> @where = null);
        //Task<bool> ExistAsync(Expression<Func<T, bool>> @where = null);
        //T GetSingle(Guid key);
        //Task<T> GetSingleAsync(Guid key);
        //T GetSingleOrDefault(Expression<Func<T, bool>> @where = null);
        //Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> @where = null);
        //IQueryable<T> Get(Expression<Func<T, bool>> @where = null);
        //Task<List<T>> GetAsync(Expression<Func<T, bool>> @where = null);
        //IEnumerable<T> GetByPagination(Expression<Func<T, bool>> @where, int pageSize, int pageIndex, bool asc = true,
        //    params Func<T, object>[] @orderby);
        //#endregion
    }
}
