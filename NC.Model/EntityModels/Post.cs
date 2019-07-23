using System;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// 帖子
    /// </summary>
    public class Post : EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
