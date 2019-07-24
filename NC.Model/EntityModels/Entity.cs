using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class Entity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public virtual TKey Id { get; set; }
    }
}
