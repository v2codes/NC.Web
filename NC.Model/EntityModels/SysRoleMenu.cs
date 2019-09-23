using System;
using System.Collections.Generic;
using System.Text;
using NC.Model.EntityModels.Base;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 角色菜单关联
    /// </summary>
    public class SysRoleMenu : EntityBase
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? RoleId { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid? MenuId { get; set; }
    }
}
