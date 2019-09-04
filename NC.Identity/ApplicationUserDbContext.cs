using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NC.Identity.Models;
using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Identity
{
    /// <summary>
    /// Identity 数据库上下文
    /// </summary>
    public class ApplicationUserDbContext : IdentityDbContext<SysUser, SysRole, Guid>
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
            : base(options)
        {

        }
    }
}
