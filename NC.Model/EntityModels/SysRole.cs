using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using NC.Core.Entities;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 角色
    /// </summary>
    public class SysRole : IdentityRole<Guid>, IEntity<Guid>
    {
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid ModifyUserId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
