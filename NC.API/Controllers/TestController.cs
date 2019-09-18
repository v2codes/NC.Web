using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NC.Core.Repositories;
using NC.Core.Services;
using NC.Model.EntityModels;
using NC.Service;
//using NC.Common.Log;
using NC.Common.Controller;
using NC.Common.Exceptions;

namespace NC.API.Controllers
{
    //[Route("api/[controller]")]
    //[Authorize]
    public class TestController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IRepository<Blog, Guid> _repository;
        private readonly IService<Blog, Guid> _service;
        public TestController(ILogger<TestController> logger, IRepository<Blog, Guid> repository, IService<Post, Guid> service) // , IRepository<Blog,Guid> repository
        {
            _logger = logger;
            //_repository = repository;
            //_service = service;
        }

        ////! 注入IUnitOfWork
        //private readonly IUnitOfWork _unitOfWork;
        //public ValuesController(IUnitOfWork unitOfWork, ILogger<ValuesController> logger)
        //{
        //    //_logger = logger;
        //    _unitOfWork = unitOfWork;
        //    var repo = unitOfWork.GetRepository<Post>();
        //    var cusRepo = unitOfWork.GetRepository<Post>(true);
        //}

        #region default actions
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //_logger.LogInformation();
            var userId = this.LoginUserId;
            return this.Success(new string[] { "value1", "value2" });
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getexception")]
        public ActionResult<IEnumerable<string>> GetException()
        {
            // return this.Success("测试异常捕捉");
            throw new Exception("这是一个自定义且未经处理的异常...");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

    }
}
