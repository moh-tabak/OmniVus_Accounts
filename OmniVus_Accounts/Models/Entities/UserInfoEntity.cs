using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniVus_Accounts.Models.Entities
{

    [Table("AspNetUsersInfo")]
    public class UserInfoEntity
    {
        [Key, ForeignKey(nameof(Account))]
        public string AccountId { get; set; } = string.Empty;
        public virtual IdentityUser Account { get; set; } = new ();

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [PersonalData]
        [Column(TypeName ="nvarchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [PersonalData] 
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public virtual AddressEntity Address { get; set; } = new ();

        public string? Picture { get; set; }

    }
}
