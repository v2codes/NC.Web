using System;
using NC.Core.Entities;
using NC.Model.Enums;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 所有数据表实体类都必须继承此类
    /// </summary>
    [Serializable]
    public class EntityBase : Entity<Guid>
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
