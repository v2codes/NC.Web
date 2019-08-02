using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Core.Entities
{
    /// <summary>
    /// 实体抽象类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public virtual TKey Id { get; set; }
    }

}
