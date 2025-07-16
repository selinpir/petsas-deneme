using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petsas2.Models;
using System;

namespace petsas2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //+
        public DbSet<Category> Categories { get; set; }
        //+
        public DbSet<SubCategory> SubCategories { get; set; }
        //
        public DbSet<Product> Products { get; set; }
        //brand+
        public DbSet<Brand> Brands { get; set; }


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
            // Category silinmeye �al��t���nda, h�l� ona ba�l� SubCategory kay�tlar� varsa silmeye izin vermeyecek
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

            // IsDeleted == false olanlar� her sorguda otomatik filtrele
            modelBuilder.Entity<Product>()
                        .HasQueryFilter(p => !p.IsDeleted);

            //GUID � OTO OLU�TUR
            modelBuilder.Entity<Product>()
                        .Property(p => p.Barcode)
                        .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Product>()
            .Property(p => p.Weight)
            .HasPrecision(5, 2);
        }
    }
}