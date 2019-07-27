using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NC.Model.EntityModels;
using NC.Model.Repository;
using NC.Web.Common.Attributes;

namespace NC.Service
{
    [DI(ServiceLifetime.Scoped, typeof(IService<,>))]
    public class PostService : ServiceBase<Post, Guid>
    {
        public PostService(IRepository<Post, Guid> repository)
            : base(repository)
        {
        }
    }
}
