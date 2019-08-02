using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NC.Core.Database
{
    /// <summary>
    /// Create db context instance
    /// </summary>
    public class CTXFactory : IDesignTimeDbContextFactory<CTX>
    {
        /// <summary>
        /// create
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CTX CreateDbContext(string[] args)
        {
            var dbContext = new CTX(new DbContextOptionsBuilder<CTX>().UseSqlServer(
                 new ConfigurationBuilder()
                   .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                   .Build()
                   .GetConnectionString("DefaultConnection")
               ).Options);
            dbContext.Database.Migrate();
            return dbContext;
        }
    }
}
