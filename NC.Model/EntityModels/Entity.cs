using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 泛型实体基类
    /// </summary>
    /// <typeparam name="TPrimary">主键类型</typeparam>
    public abstract class Entity<TPrimary>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public virtual TPrimary Id { get; set; }
    }
}
