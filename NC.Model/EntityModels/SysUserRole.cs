using System;
using System.Collections.Generic;
using System.Text;
using NC.Model.EntityModels.Base;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    public class SysUserRole : EntityBase
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? RoleId { get; set; }
    }
}
