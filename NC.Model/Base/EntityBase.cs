using System;
using NC.Core.Entities;
using NC.Model.Enums;

namespace NC.Model.EntityModels.Base
{
    /// <summary>
    /// 所有数据表实体类都必须继承此类
    /// </summary>
    [Serializable]
    public abstract class EntityBase : IEntity<Guid>
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            Status = (int)StatusEnum.Normal;
            CreateDate = DateTime.Now;
        }
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? Status { get; set; }
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
