using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Shared
{
    public class UserPasswordChange
    {
        [Required, StringLength(100, MinimumLength =6)]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password",ErrorMessage ="Passwords do not match!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
