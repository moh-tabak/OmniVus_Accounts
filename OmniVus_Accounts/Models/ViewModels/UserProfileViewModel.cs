
using System.ComponentModel.DataAnnotations;

namespace OmniVus_Accounts.Models.ViewModels
{
#nullable disable
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
        }

        public UserProfileViewModel(IEnumerable<string> roles, string firstName, string lastName, string email, string phone, string street, string postalCode, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Street = street;
            PostalCode = postalCode;
            City = city;
            Roles = roles;
        }

        public IEnumerable<string> Roles { get; set; }

        public string ProfilePicName { get; set; }

        public IFormFile ProfilePic { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "Name is either too short, or too long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "Name is either too short, or too long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} is invalid", MinimumLength = 6)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "Enter a valid {0}, and a street number", MinimumLength = 5)]
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(10, ErrorMessage = "{0} must be atleats {2} characters long, and max {1}.", MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, ErrorMessage = "Name is either too short, or too long.", MinimumLength = 2)]
        public string City { get; set; }

    }
}
