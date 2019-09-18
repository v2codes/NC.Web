using System;
using Microsoft.Extensions.DependencyInjection;
using NC.Common.Attributes;
using NC.Core.Database;
using NC.Core.Repositories;
using NC.Model.EntityModels;

namespace NC.Repository
{
    /// <summary>
    /// Blog 仓储类
    /// </summary>
    [DI(ServiceLifetime.Scoped, typeof(IRepository<,>))]
    public class BlogRepository : BaseRepository<Blog, Guid>
    {
        public BlogRepository(CTX dbContext)
            : base(dbContext)
        {
        }
    }
}
