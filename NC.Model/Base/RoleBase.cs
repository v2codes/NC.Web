using System;
using Microsoft.AspNetCore.Identity;
using NC.Core.Entities;
using NC.Model.Enums;

namespace NC.Model.EntityModels.Base
{
    /// <summary>
    /// 自定义 IdentityRole 抽象基类
    /// </summary>
    public abstract class RoleBase : IdentityRole<Guid>, IEntity<Guid>
    {
        public RoleBase()
        {
            Id = Guid.NewGuid();
            Status = (int)EnumStatus.Normal;
            CreateDate = DateTime.Now;
        }
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
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
