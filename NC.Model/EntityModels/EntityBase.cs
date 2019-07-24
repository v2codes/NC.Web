using System;
using System.ComponentModel.DataAnnotations;
using NC.Model.Enums;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 所有数据表实体类都必须继承此类
    /// </summary>
    [Serializable]
    public abstract class EntityBase : Entity<Guid>
    {
        /// <summary>
        /// 状态
        /// </summary>
        public abstract StatusEnum Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public abstract DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public abstract Guid? CreateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public abstract DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public abstract Guid ModifyUserId { get; set; }
    }
}
