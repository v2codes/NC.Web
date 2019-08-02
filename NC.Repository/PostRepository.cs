using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NC.Core.Attributes;
using NC.Core.Database;
using NC.Core.Repositories;
using NC.Model.EntityModels;

namespace NC.Repository
{
    [DI(ServiceLifetime.Scoped, typeof(IRepository<,>))]
    public class PostRepository : BaseRepository<Post, Guid>
    {
        public PostRepository(CTX dbContext) : base(dbContext)
        {
        }
    }
}
