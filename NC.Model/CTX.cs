using Microsoft.EntityFrameworkCore;
using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Model
{
    /// <summary>
    /// Db Context
    /// </summary>
    public class CTX : DbContext
    {
        public CTX(DbContextOptions<CTX> options)
                : base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
