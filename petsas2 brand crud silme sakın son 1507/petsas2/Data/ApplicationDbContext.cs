using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petsas2.Models;

namespace petsas2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        //public DbSet<Product> Products { get; set; }
        public DbSet<ProductNew> ProductNews { get; set; }

        //brand+
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        //silinmeyi engellemek icin
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Category-subcategory
            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // Category silinmeye çalýþtýðýnda, hâlâ ona baðlý SubCategory kayýtlarý varsa silmeye izin vermeyecek
            //subcategory-product
            modelBuilder.Entity<ProductNew>()
                .HasOne(p => p.SubCategory)
                .WithMany(sc => sc.ProductNews)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //product-brand 
            modelBuilder.Entity<ProductNew>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.ProductNews)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            //product-productfeature
            modelBuilder.Entity<ProductFeature>()
                .HasOne(f => f.ProductNew)
                .WithMany(p => p.Features)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductNew>(entity =>
            {
                entity.Property(e => e.NetBirimFiyat)
                      .HasPrecision(18, 4);

                entity.Property(e => e.DefaultKdvOraný)
                     .HasPrecision(18, 4);
            });


        }
    }
}