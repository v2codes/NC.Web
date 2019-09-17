using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NC.Common.Controller;
using NC.Core.Services;
using NC.Model.EntityModels;

namespace NC.API.Controllers
{
    /// <summary>
    /// 用户相关
    /// </summary>
    [Authorize]
    public class UserController : BaseController
    {
        /// <summary>
        /// Service
        /// </summary>
        private readonly IService<SysUser, Guid> userService = null;
        public UserController(ILogger<UserController> logger, IService<SysUser, Guid> service)
            : base(logger)
        {
            userService = service;
        }

        /// <summary>
        /// 加载所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getallusers")]
        public IActionResult GetAllUsers()
        {
            var res = userService.Query(p => p.Id == Guid.Parse("9172d6d1-78fa-45f1-b5f5-35132963cc8d")).ToList();
            return this.Success(res);
        }
    }
}