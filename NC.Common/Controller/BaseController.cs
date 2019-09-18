using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NC.Common.Controller
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 是否已登录
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User.Identity.IsAuthenticated;
            }
        }
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        public Guid? LoginUserId
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    return Guid.Parse(userId);
                }
                return null;
            }
        }
        /// <summary>
        /// 当前登录用户名
        /// </summary>
        public string LoginUserName
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.FindFirstValue(ClaimTypes.Name);
                    return userName;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger<BaseController> logger;

        /// <summary>
        /// 应用配置
        /// </summary>
        protected readonly IConfiguration configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_logger">注入日志工具</param>
        /// <param name="_configuration">注入配置文件</param>
        public BaseController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_logger">注入日志工具</param>
        /// <param name="_configuration">注入配置文件</param>
        public BaseController(ILogger<BaseController> _logger)
        {
            logger = _logger;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_logger">注入日志工具</param>
        /// <param name="_configuration">注入配置文件</param>
        public BaseController(ILogger<BaseController> _logger, IConfiguration _configuration)
        {
            logger = _logger;
            configuration = _configuration;
        }
    }
}
