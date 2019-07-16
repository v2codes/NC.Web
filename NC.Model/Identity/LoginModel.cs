using System;
using System.ComponentModel.DataAnnotations;

namespace NC.Model.Identity
{
    /// <summary>
    /// 登录Model
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
