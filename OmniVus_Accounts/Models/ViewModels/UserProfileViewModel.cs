using Microsoft.AspNetCore.Identity;
using OmniVus_Accounts.Data;
using OmniVus_Accounts.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace OmniVus_Accounts.Models.ViewModels
{
#nullable disable
    public class UserProfileViewModel
    {
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

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
    }
}
