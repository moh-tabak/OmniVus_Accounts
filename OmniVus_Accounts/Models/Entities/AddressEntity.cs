using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmniVus_Accounts.Models.Entities
{
#nullable disable

    [Table("AspNetAddresses")]
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string Street { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string PostalCode { get; set; }
    }
}
