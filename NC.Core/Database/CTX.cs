using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NC.Core.Entities;

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

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }
    }
}
