using System;
using System.Collections.Generic;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// blog
    /// </summary>
    public class Blog: EntityBase
    {
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
