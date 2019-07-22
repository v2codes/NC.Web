using System;
using System.ComponentModel.DataAnnotations;
using NC.Model.Enums;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public abstract class EntityBase:Entity<Guid>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public virtual StatusEnum Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual Guid? CreateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public virtual Guid? ModifyUserId { get; set; }
    }
}
