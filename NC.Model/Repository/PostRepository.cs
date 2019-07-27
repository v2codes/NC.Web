using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NC.Model.EntityModels;
using NC.Web.Common.Attributes;

namespace NC.Model.Repository
{
    [DI(ServiceLifetime.Scoped, typeof(IRepository<,>))]
    public class PostRepository : RepositoryBase<Post, Guid>
    {
        public PostRepository(CTX dbContext) : base(dbContext)
        {
        }
    }
}
