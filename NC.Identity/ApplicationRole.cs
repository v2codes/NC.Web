using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Identity
{
    /// <summary>
    /// 自定义角色
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
