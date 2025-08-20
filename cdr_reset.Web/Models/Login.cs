using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace cdr_reset.Web.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Enter your employee number")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}