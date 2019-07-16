using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NC.Identity
{
    /// <summary>
    /// Application User
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }
        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }
    }
}
