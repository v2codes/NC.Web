using Microsoft.AspNetCore.Identity;
using NC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 用户
    /// </summary>
    public class SysUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid ModifyUserId { get; set; }
    }
}
