using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookDatabase.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Email format is not valid")]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        
        public string Password { get; set; }

        public bool IsValidPassword(string password)
        {
            return Password == password;
        }
    }
}
