using System;
using Microsoft.Extensions.DependencyInjection;
using NC.Common.Attributes;
using NC.Core.Repositories;
using NC.Core.Services;
using NC.Model.EntityModels;

namespace NC.Service
{
    /// <summary>
    /// User Service
    /// </summary>
    [DI(ServiceLifetime.Scoped, typeof(IService<,>))]
    public class UserService : BaseService<SysUser, Guid>
    {
        public UserService(IRepository<SysUser, Guid> repository)
            : base(repository)
        {
        }
    }
}
