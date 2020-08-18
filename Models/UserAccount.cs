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
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 120 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
