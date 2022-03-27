using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace OmniVus_Accounts.Models
{
#nullable disable
    public class SignUpForm
    {

        [Display(Name ="First Name")]
        [Required(ErrorMessage ="{0} is required")]
        [StringLength(50, ErrorMessage = "Name is either too short, or too long.",MinimumLength =2)]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "Name is either too short, or too long.", MinimumLength =2)]
        public string LastName { get; set; }

        [Display(Name ="Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} is invalid",MinimumLength =6)]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} must contain atleast {2} charachter, of which an upper-case letter and a digit.", MinimumLength =6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage ="The password doesn't match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "Enter a valid {0}, and a street number", MinimumLength =5)]
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(10, ErrorMessage = "{0} must be atleats {2} characters long, and max {1}.",MinimumLength =5)]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "Name is either too short, or too long.",MinimumLength =2)]
        public string City { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage ="You must agree in order to use our services.")]
        public bool PolicyCheck { get; set; }
    }
}
