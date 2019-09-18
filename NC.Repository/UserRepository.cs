using System;
using Microsoft.Extensions.DependencyInjection;
using NC.Model.EntityModels;
using NC.Core.Repositories;
using NC.Core.Database;
using NC.Common.Attributes;

namespace NC.Repository
{
    /// <summary>
    /// Post 仓储类
    /// </summary>
    [DI(ServiceLifetime.Scoped, typeof(IRepository<,>))]
    public class UserRepository : BaseRepository<SysUser, Guid>
    {
        public UserRepository(CTX db) 
            : base(db)
        {
        }
    }
}
