﻿using NC.Core.Entities;
using System;
using System.Collections.Generic;

namespace NC.Model.EntityModels
{
    /// <summary>
    /// blog
    /// </summary>
    public class Blog : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid ModifyUserId { get; set; }

        public string Url { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
