using System.ComponentModel.DataAnnotations;

namespace OmniVus_Accounts.Models
{
#nullable disable
    public class LoginForm
    {

        public string ReturnUrl { get; set; }
        public string ErrorMsg { get; set; }

        [Display(Name ="Email")]
        [Required(ErrorMessage ="The {0} field can't be empty!")]
        public string Email { get; set; }

        [Display(Name ="Password")]
        [Required(ErrorMessage = "The {0} field can't be empty!")]
        public string Password { get; set; }
    }
}
