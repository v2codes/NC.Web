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
    /// Post Service
    /// </summary>
    [DI(ServiceLifetime.Scoped, typeof(IService<,>))]
    public class PostService : BaseService<Post, Guid>
    {
        public PostService(IRepository<Post, Guid> repository)
            : base(repository)
        {
        }
    }
}
