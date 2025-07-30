using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        //---------------------------------------------------------
        //urunler ile ilgil kýsým
        //kategori+
        public DbSet<Category> Categories { get; set; }
        //altkategori+
        public DbSet<SubCategory> SubCategories { get; set; }
        //urunler+
        public DbSet<Product> Products { get; set; }
        //marka+
        public DbSet<Brand> Brands { get; set; }
        //fiyatlandýrma -
        public DbSet<Pricing> Pricings { get; set; }
        //urunler ile ilgil kýsým
        //---------------------------------------------------------
        public DbSet<SepetDetay> Sepets { get; set; }
        public DbSet<KullaniciSepet> KullaniciSepets { get; set; }
        //---------------------------------------------------------
        //kullanýcý ile ilgili kýsým
        public DbSet<HesapBilgileri> hesapBilgileris { get; set; }
        public DbSet<AdresBilgileri> AdresBilgileris { get; set; }
        public DbSet<PetBilgileri> PetBilgileri { get; set; }



        //enum tipler icin db set tanýmlanmadý cünkü dbset veri tabanýnda bir tabloya karþýlýk gelen sýnýflar için tutulur
        //burada enumlar tablo deðil ilgili tablolarýn bir sutunudur
        //kullanýcý ile ilgili kýsým
        //---------------------------------------------------------



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //silinmeyi engellemek icin
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
            //-----------------------------------------------------------
            //ISDELETED KISMI BURAYA YAZ
            // IsDeleted == false olanlarý her sorguda otomatik filtrele
            modelBuilder.Entity<Product>()
                        .HasQueryFilter(p => !p.IsDeleted);

            //ürün fiyat silinenleri getirme
            modelBuilder.Entity<Pricing>()
            .HasQueryFilter(pr => !pr.Product.IsDeleted);
            //ISDELETED KISMI BURAYA YAZ
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //GUID Ý OTO OLUÞTUR
            modelBuilder.Entity<Product>()
                        .Property(p => p.Barcode)
                        .HasDefaultValueSql("NEWID()");
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //product-weight : agýrlýk decimal hata vermemesi icin
            modelBuilder.Entity<Product>()
            .Property(p => p.Weight)
            .HasPrecision(8, 2);
            //-----------------------------------------------------------
            //decimal vs icin fiyatlandýrma 
            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.ToTable("Pricing");
                entity.HasKey(p => p.FiyatId);

                entity.Property(p => p.NetFiyat)
                .HasPrecision(18, 2) //fiyatta genelde bu
                .IsRequired();

                entity.Property(p => p.IndirimOrani)
                .HasPrecision(5, 4) //kdv yüzde fln gibilerde bu
               .IsRequired(false);

                entity.Property(p => p.KdvOrani)
                .HasPrecision(5, 4)
                .IsRequired();

                //insert sýrasýnda o anki tarih alýnýr
                entity.Property(p => p.FiyatBaslangicTarihi)
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd()
                 .IsRequired();

                //FÝYAT DEÐÝÞTÝRMEDE KOD TARAFINDA ESKÝ FÝYATIN BÝTÝÞ TARÝHÝNÝ ÞÝMDÝKÝ ZAMAN YAP
                //herhangi deger girmicez
                entity.Property(p => p.FiyatSonlanmaTarihi)
                         .IsRequired(false);


                //ayný tarihli fiyatlarýn çakýþmamasý icin günde 2 kere fiyat girdin mesela
                entity.HasIndex(p => new { p.ProductId, p.FiyatBaslangicTarihi })
                      .IsUnique();

                // FK iliþkisi
                entity.HasOne(p => p.Product)
                 .WithMany(p => p.Pricings)
                 .HasForeignKey(p => p.ProductId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
            //-----------------------------------------------------------
            modelBuilder.Entity<SepetDetay>()
              .Property(s => s.BrutFiyat)
              .HasPrecision(8, 2);
            //-----------------------------------------------------------
            //CÝNSÝYET - HESAP DURUMU
            //enumlarýn sayýsal deðil metinsel gelmesi icin:
            modelBuilder.Entity<HesapBilgileri>()
                .Property(h => h.CinsiyetTipi)
                .HasConversion<string>();  //Sayýsal olsaydý .HasConversion<int>() 

            modelBuilder.Entity<HesapBilgileri>()
                .Property(h=>h.HesapDurumu)
                .HasConversion<string>();
            //-----------------------------------------------------------
            //il ve ilce tablolarý daha onceden sqlde oluþturulmuþtu
            //migrationda ignore edildi          
            modelBuilder.Ignore<il>();
            modelBuilder.Ignore<ilce>();
            //-----------------------------------------------------------
        }
    }
}