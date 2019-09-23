using System;
using System.Collections.Generic;
using System.Text;
using NC.Model.EntityModels.Base;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysMenu : EntityBase
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 父级菜单
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? SortIndex { get; set; }
    }
}
