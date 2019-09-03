using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NC.Core.Attributes;
using NC.Core.Repositories;
using NC.Core.Services;
using NC.Model.EntityModels;

namespace NC.Service
{
    /// <summary>
    /// Blog Service
    /// </summary>
    [DI(ServiceLifetime.Scoped, typeof(IService<,>))]
    public class BlogService : BaseService<Blog, Guid>
    {
        public BlogService(IRepository<Blog, Guid> repository)
            : base(repository)
        {
        }
    }
}
