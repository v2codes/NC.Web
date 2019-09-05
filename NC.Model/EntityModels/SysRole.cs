using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using NC.Core.Entities;
using NC.Model.EntityModels.Base;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 角色
    /// </summary>
    public class SysRole : RoleBase
    {
        /// <summary>
        /// 是否默认角色
        /// </summary>
        public virtual bool? IsDefault{ get; set; }
        /// <summary>
        /// 是否系统角色
        /// </summary>
        public virtual bool? IsSystem{ get; set; }
    }
}
