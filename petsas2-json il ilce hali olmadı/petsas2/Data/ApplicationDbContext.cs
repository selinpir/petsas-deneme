using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petsas2.Models;
using petsas2.Models.UserViewModel;

namespace petsas2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Province → District
            builder.Entity<District>()
                   //her ilçenin bir ili var
                   .HasOne(d => d.Provinces)
                   .WithMany(p => p.Districts)
                   .HasForeignKey(d => d.ProvinceId)
                   // İl silindiğinde ilçeleri silmeyelim:
                   .OnDelete(DeleteBehavior.Restrict);
            //Province silinmek istendiğinde, ona bağlı District kayıtlarının otomatik silinmesini engelliyoruz  

            // Province → UserProfile
            builder.Entity<UserProfile>()
                   .HasOne(up => up.Province)
                   .WithMany()
                   .HasForeignKey(up => up.ProvinceId)
                   // İl silindiğinde kullanıcı profilini silmeyelim:
                   .OnDelete(DeleteBehavior.Restrict);

            // District → UserProfile
            builder.Entity<UserProfile>()
                   .HasOne(up => up.District)
                   .WithMany()
                   .HasForeignKey(up => up.DistrictId)
                   // İlçe silindiğinde kullanıcı profilini silmeyelim:
                   .OnDelete(DeleteBehavior.Restrict);
            ////DeleteBehavior.Restrict — SQL’de ON DELETE NO ACTION — ile hiçbir ilişkide otomatik silme yapılamaz
        }

    }

}
