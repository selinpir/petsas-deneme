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
        //urunler ile ilgil k�s�m
        //kategori+
        public DbSet<Category> Categories { get; set; }
        //altkategori+
        public DbSet<SubCategory> SubCategories { get; set; }
        //urunler+
        public DbSet<Product> Products { get; set; }
        //marka+
        public DbSet<Brand> Brands { get; set; }
        //fiyatland�rma -
        public DbSet<Pricing> Pricings { get; set; }
        //urunler ile ilgil k�s�m
        //---------------------------------------------------------
        public DbSet<SepetDetay> Sepets { get; set; }
        public DbSet<KullaniciSepet> KullaniciSepets { get; set; }
        //---------------------------------------------------------
        //kullan�c� ile ilgili k�s�m
        public DbSet<HesapBilgileri> hesapBilgileris { get; set; }
        public DbSet<AdresBilgileri> AdresBilgileris { get; set; }
        public DbSet<PetBilgileri> PetBilgileri { get; set; }



        //enum tipler icin db set tan�mlanmad� c�nk� dbset veri taban�nda bir tabloya kar��l�k gelen s�n�flar i�in tutulur
        //burada enumlar tablo de�il ilgili tablolar�n bir sutunudur
        //kullan�c� ile ilgili k�s�m
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
            //-----------------------------------------------------------
            //ISDELETED KISMI BURAYA YAZ
            // IsDeleted == false olanlar� her sorguda otomatik filtrele
            modelBuilder.Entity<Product>()
                        .HasQueryFilter(p => !p.IsDeleted);

            //�r�n fiyat silinenleri getirme
            modelBuilder.Entity<Pricing>()
            .HasQueryFilter(pr => !pr.Product.IsDeleted);
            //ISDELETED KISMI BURAYA YAZ
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //GUID � OTO OLU�TUR
            modelBuilder.Entity<Product>()
                        .Property(p => p.Barcode)
                        .HasDefaultValueSql("NEWID()");
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //product-weight : ag�rl�k decimal hata vermemesi icin
            modelBuilder.Entity<Product>()
            .Property(p => p.Weight)
            .HasPrecision(8, 2);
            //-----------------------------------------------------------
            //decimal vs icin fiyatland�rma 
            modelBuilder.Entity<Pricing>(entity =>
            {
                entity.ToTable("Pricing");
                entity.HasKey(p => p.FiyatId);

                entity.Property(p => p.NetFiyat)
                .HasPrecision(18, 2) //fiyatta genelde bu
                .IsRequired();

                entity.Property(p => p.IndirimOrani)
                .HasPrecision(5, 4) //kdv y�zde fln gibilerde bu
               .IsRequired(false);

                entity.Property(p => p.KdvOrani)
                .HasPrecision(5, 4)
                .IsRequired();

                //insert s�ras�nda o anki tarih al�n�r
                entity.Property(p => p.FiyatBaslangicTarihi)
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd()
                 .IsRequired();

                //F�YAT DE���T�RMEDE KOD TARAFINDA ESK� F�YATIN B�T�� TAR�H�N� ��MD�K� ZAMAN YAP
                //herhangi deger girmicez
                entity.Property(p => p.FiyatSonlanmaTarihi)
                         .IsRequired(false);


                //ayn� tarihli fiyatlar�n �ak��mamas� icin g�nde 2 kere fiyat girdin mesela
                entity.HasIndex(p => new { p.ProductId, p.FiyatBaslangicTarihi })
                      .IsUnique();

                // FK ili�kisi
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
            //C�NS�YET - HESAP DURUMU
            //enumlar�n say�sal de�il metinsel gelmesi icin:
            modelBuilder.Entity<HesapBilgileri>()
                .Property(h => h.CinsiyetTipi)
                .HasConversion<string>();  //Say�sal olsayd� .HasConversion<int>() 

            modelBuilder.Entity<HesapBilgileri>()
                .Property(h=>h.HesapDurumu)
                .HasConversion<string>();
            //-----------------------------------------------------------
            //il ve ilce tablolar� daha onceden sqlde olu�turulmu�tu
            //migrationda ignore edildi          
            modelBuilder.Ignore<il>();
            modelBuilder.Ignore<ilce>();
            //-----------------------------------------------------------
        }
    }
}