using Microsoft.Extensions.DependencyInjection;
using NC.Core.Attributes;
using NC.Core.Repositories;
using NC.Core.Services;
using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
