using NC.Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NC.Model.Repository
{
    public class BlogRepository : RepositoryBase<Blog, Guid>
    {
        public BlogRepository(CTX dbContext)
            : base(dbContext)
        {
        }
    }
}
