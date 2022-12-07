using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

    }
}
