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
        public DbSet<Product> Products { get; set; }

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
            modelBuilder.Entity<Product>()
                .HasOne(p => p.SubCategory)
                .WithMany(sc => sc.Products)
                .HasForeignKey(p => p.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //product-brand 
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            //product-productfeature
            modelBuilder.Entity<ProductFeature>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Features)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>(entity =>
            {
                //fiyatlar
                entity.Property(p => p.Price)
                .HasPrecision(18, 2);

                entity.Property(p => p.DiscountedPrice)
                .HasPrecision(18, 2);

                entity.Property(p => p.KdvPrice)
                .HasPrecision(18, 2);
                //oranlar
                entity.Property(p => p.DiscountedRatio)
                .HasPrecision(5, 2);

                entity.Property(p => p.KdvRatio)
               .HasPrecision(5, 2);

                entity.Property(p => p.Rating)
               .HasPrecision(3, 2);
            });
        }

    }
}