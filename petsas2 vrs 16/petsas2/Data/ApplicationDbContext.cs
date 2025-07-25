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
        //--------------------------------------------------------------------------------
        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Pricing> Pricings { get; set; }

        //--------------------------------------------------------------------------------
        //hesap ekle +
        //pet ekle +
        //pet sil +
        //adres ekle +
        public DbSet<HesapBilgileri> HBilgileri { get; set; }
        public DbSet<PetBilgileri> PBilgileri { get; set; }
        public DbSet<AdresBilgileri> ABilgileri { get; set; }
        public DbSet<Il> IlDb { get; set; }
        public DbSet<Ilce> IlceDb { get; set; }
        //--------------------------------------------------------------------------------        
        public DbSet<SepetDetay> Sepets { get; set; }
        public DbSet<KullaniciSepet> KullaniciSepets { get; set; }
        //--------------------------------------------------------------------------------

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //-----YENÝ---------------------------------------------------------------------------



            //SEPET EKSÝK-------------------------------------------------------------------------
            modelBuilder.Entity<SepetDetay>()
              .Property(s => s.BrutFiyat)
              .HasPrecision(8, 2);
            //-----ESKÝ---------------------------------------------------------------------------
            // Ýl-Ýlçe iliþkisi
            modelBuilder.Entity<Ilce>()
                .HasOne(i => i.Il)
                .WithMany(i => i.Ilceler)
                .HasForeignKey(i => i.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres-Ýlçe iliþkisi
            modelBuilder.Entity<AdresBilgileri>()
                .HasOne(a => a.Ilce)
                .WithMany()
                .HasForeignKey(a => a.IlceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres-Ýl iliþkisi
            modelBuilder.Entity<AdresBilgileri>()
                .HasOne(a => a.Il)
                .WithMany()
                .HasForeignKey(a => a.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            //bir kullanýcýnýn bir adresi olacak þekilde deðiþtirdim 
            modelBuilder.Entity<HesapBilgileri>()
                 .HasOne(h => h.AdresBilgisi)
                 .WithOne(a => a.HesapBilgileri)
                 .HasForeignKey<AdresBilgileri>(a => a.HesapBilgileriId);
        
            //CÝNSÝYET - HESAP DURUMU
            //enumlarýn sayýsal deðil metinsel gelmesi icin:
            modelBuilder.Entity<HesapBilgileri>()
                .Property(h => h.CinsiyetTipi)
                .HasConversion<string>();  //Sayýsal olsaydý .HasConversion<int>() 

            modelBuilder.Entity<HesapBilgileri>()
                .Property(h => h.HesapDurumu)
                .HasConversion<string>();
           
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
           
            //-----------------------------------------------------------
        }
    }
}