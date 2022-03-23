using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter Your FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please Enter a Valid Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [Compare("ConfirmPassword", ErrorMessage = "Passwords are not Matching")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm Your Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
