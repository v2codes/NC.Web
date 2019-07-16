using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Model.EntityModels
{
    public class Blog
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
