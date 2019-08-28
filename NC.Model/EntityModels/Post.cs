using NC.Core.Entities;
using System;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 帖子
    /// </summary>
    public class Post : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid ModifyUserId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
