using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniVus_Accounts.Models.Entities
{
#nullable disable

    [Table("AspNetUsersInfo")]
    public class UserInfoEntity
    {
        [Key, ForeignKey(nameof(Account))]
        public string AccountId { get; set; }
        public virtual IdentityUser Account { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName ="nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName ="nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [PersonalData]
        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual AddressEntity Address { get; set; }       
    }
}
