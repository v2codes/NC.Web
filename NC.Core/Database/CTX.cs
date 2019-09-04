using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NC.Core.Entities;
using NC.Core.Helper;

namespace NC.Core.Database
{
    /// <summary>
    /// db 访问基类
    /// </summary>
    public class CTX : DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public CTX(DbContextOptions<CTX> options)
               : base(options)
        {

        }

        /// <summary>
        /// 动态添加实体模型
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //取基类=EntityBase 
            var modelTypes = ReflectionHelper.GetTypesByAssemblyName("NC.Model");
            foreach (var item in modelTypes)
            {
                // if (item.BaseType == typeof(IEntity<>))
                if (!item.IsAbstract && item.HasImplementedRawGeneric(typeof(IEntity<>)))
                {
                    modelBuilder.Model.GetOrAddEntityType(item);
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }
    }
}
