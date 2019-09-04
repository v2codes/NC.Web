using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Core.Entities
{
    /// <summary>
    /// 实体类接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        TKey Id { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        int? Status { get; set; }
    }
}
