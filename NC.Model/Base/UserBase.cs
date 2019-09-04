﻿using System;
using Microsoft.AspNetCore.Identity;
using NC.Core.Entities;
using NC.Model.Enums;

namespace NC.Model.EntityModels.Base
{
    /// <summary>
    /// 自定义 IdentityUser 抽象基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class UserBase : IdentityUser<Guid>, IEntity<Guid>
    {
        public UserBase()
        {
            Id = Guid.NewGuid();
            Id = Guid.NewGuid();
            Status = (int)StatusEnum.Normal;
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
    }
}