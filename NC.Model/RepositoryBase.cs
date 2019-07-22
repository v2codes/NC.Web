using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NC.Model.EntityModels;

namespace NC.Model
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        /// <summary>
        /// dbcontext
        /// </summary>
        private readonly CTX _dbContext;
        private DbSet<T> _entities;

        /// <summary>
        /// constructor
        /// </summary>
        public RepositoryBase(CTX dbContext) => _dbContext = dbContext;


        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public List<T> GetAllList()
        {
            return GetAll().ToList();
        }

        public List<T> GetAllList(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public async Task<List<T>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _dbContext.Set<T>();
                }
                return _entities;
            }
        }

        /// <summary>
        /// 实现泛型接口中的IQueryable<T>类型的 Table属性
        /// 标记为virtual是为了可以重写它
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public T GetById(object id) => this.Entities.Find(id);

        public void Insert(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    this.Entities.Add(model);
                    this._dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T model)
        {
            try
            {
                //model为空，抛空异常
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    //直接保存了
                    this._dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(model);
                this._dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
