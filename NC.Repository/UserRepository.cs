using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using NC.Model.EntityModels;
using NC.Core.Attributes;
using NC.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using NC.Core.Database;

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
