using NC.Model.EntityModels;
using NC.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Service
{
    public class BlogService : ServiceBase<Blog, Guid>
    {
        public BlogService(IRepository<Blog, Guid> repository)
            : base(repository)
        {
        }
    }
}
