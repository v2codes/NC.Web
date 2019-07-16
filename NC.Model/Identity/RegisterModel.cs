using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NC.Model.Identity
{
    /// <summary>
    /// 注册Model
    /// </summary>
    public class RegisterModel : LoginModel
    {
        /// <summary>
        /// FirstName
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        /// <summary>
        /// LastName
        /// </summary>
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Required]
        [Compare("Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "密码必须至少为8个字符，并包含以下4个字符中的3个:大写(A-Z)、小写(A-Z)、数字(0-9)和特殊字符(例如!@#$%^&*)")]
        public string PasswordConfirm { get; set; }
    }
}
