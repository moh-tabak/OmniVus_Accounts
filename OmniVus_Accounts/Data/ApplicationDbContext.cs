using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OmniVus_Accounts.Models.Entities;

namespace OmniVus_Accounts.Data
{
#nullable disable
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserInfoEntity> UsersInfo { get; set; }


        public virtual DbSet<AddressEntity> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserInfoEntity>().HasOne(x=> x.Address).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
}