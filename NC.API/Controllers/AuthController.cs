using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NC.Common.Controller;
using NC.Identity.Models;
using NC.Model.EntityModels;
using NSwag.Annotations;

namespace NC.API.Controllers
{
    /// <summary>
    /// 登录授权
    /// </summary>
    [AllowAnonymous]
    [SwaggerTag("登录相关")]
    public class AuthController : BaseController
    {
        readonly UserManager<SysUser> userManager;
        readonly SignInManager<SysUser> signInManager;

        public AuthController(UserManager<SysUser> userManager, SignInManager<SysUser> signInManager, IConfiguration configuration, ILogger<AuthController> logger)
            : base(logger, configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, isPersistent: false, lockoutOnFailure: false);
            if (!loginResult.Succeeded)
            {
                return BadRequest();
            }

            var user = await userManager.FindByNameAsync(loginModel.UserName);
            return this.Success(GetToken(user));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new SysUser()
            {
                // TODO Use Automapper instead of manual binding
                UserName = registerModel.UserName,
                Status = 1,
                CreateUserId = Guid.NewGuid(),
                ModifyUserId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                Email = registerModel.Email
            };

            var identityResult = await this.userManager.CreateAsync(user, registerModel.Password);
            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }
            await this.signInManager.SignInAsync(user, isPersistent: false);
            return this.Success(GetToken(user));
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var user = await userManager.FindByNameAsync(
                User.Identity.Name ??
                User.Claims.Where(p => p.Properties.ContainsKey("unique_name")).Select(p => p.Value).FirstOrDefault()
            );
            return this.Success(GetToken(user));
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetToken(IdentityUser<Guid> user)
        {
            var utcNow = DateTime.UtcNow;
            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString())
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration.GetValue<String>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(this.configuration.GetValue<int>("Tokens:Lifetime")),
                audience: this.configuration.GetValue<String>("Tokens:Audience"),
                issuer: this.configuration.GetValue<String>("Tokens:Issuer")
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}