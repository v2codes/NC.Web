using NC.Model.EntityModels;
using NC.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NC.Service
{
    public abstract class ServiceBase<T> : IService<T> where T : EntityBase
    {
        public IRepository<T,Guid> _repository { get; set; }

        protected ServiceBase(IRepository<T, Guid> repository)
        {
            _repository = repository;
        }

        public int Add(T model)
        {
            return _repository.Add(model);
        }

        public Task<int> AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public int AddRange(IList<T> models)
        {
            return _repository.AddRange(models);
        }

        public Task<int> AddRangeAsync(IList<T> entities)
        {
            return _repository.AddRangeAsync(entities);
        }

        //public int BatchUpdate(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> BatchUpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateExp)
        //{
        //    throw new NotImplementedException();
        //}

        //public void BulkInsert(IList<T> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Count(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> CountAsync(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Delete(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Delete(Expression<Func<T, bool>> where)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> DeleteAsync(Expression<Func<T, bool>> where)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Edit(T entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public int EditRange(ICollection<T> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Exist(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> ExistAsync(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public IQueryable<T> Get(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<List<T>> GetAsync(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> GetByPagination(Expression<Func<T, bool>> where, int pageSize, int pageIndex, bool asc = true, params Func<T, object>[] orderby)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetSingle(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<T> GetSingleAsync(Guid key)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetSingleOrDefault(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(T model, params string[] updateColumns)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> UpdateAsync(Expression<Func<T, bool>> where, Expression<Func<T, T>> updateFactory)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
