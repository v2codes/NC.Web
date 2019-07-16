using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NC.Identity
{
    /// <summary>
    /// Create db context instance
    /// </summary>
    public class ApplicationUserDbContextFactory : IDesignTimeDbContextFactory<ApplicationUserDbContext>
    {
        /// <summary>
        /// create
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ApplicationUserDbContext CreateDbContext(string[] args)
        {
            var dbContext = new ApplicationUserDbContext(new DbContextOptionsBuilder<ApplicationUserDbContext>().UseSqlServer(
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
