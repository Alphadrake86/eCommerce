using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    public class UserAccount
    {
        [Key]
        public int ID { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Compare(nameof(Email))]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Username { get; set; }
    }
}
