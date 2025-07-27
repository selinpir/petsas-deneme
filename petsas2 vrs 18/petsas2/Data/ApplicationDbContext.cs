using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using petsas2.Models;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public DbSet<HesapBilgileri> HBilgileri { get; set; }
        public DbSet<PetBilgileri> PBilgileri { get; set; }
        public DbSet<AdresBilgileri> ABilgileri { get; set; }
        public DbSet<Il> IlDb { get; set; }
        public DbSet<Ilce> IlceDb { get; set; }
        //--------------------------------------------------------------------------------        
        public DbSet<SepetDetay> Sepets { get; set; }
        public DbSet<KullaniciSepet> KullaniciSepets { get; set; }
        //--------------------------------------------------------------------------------
        public DbSet<Siparis> SiparisDb { get; set; }
        public DbSet<SiparisDetay> SiparisDetayDb { get; set; }
        public DbSet<Odeme> OdemeDb { get; set; }
        public DbSet<Fatura> FaturaDb { get; set; }

        //--------------------------------------------------------------------------------

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //-----YEN�---------------------------------------------------------------------------

            modelBuilder.Entity<Siparis>()
                .HasOne(s => s.HesapBilgileri)
                .WithMany()
                .HasForeignKey(s => s.HesapBilgileriId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Siparis>()
                .HasOne(s => s.Adres)
                .WithMany()
                .HasForeignKey(s => s.AdresId)
                .OnDelete(DeleteBehavior.Restrict);

            //-----------------------------------------------------------------------------------
            //Cascade: Ana kay�t silinirse ba�l� kay�tlar da otomatik silinir 
            //Restrict: Ana kay�t silinemez, e�er ili�kili kay�t varsa hata verir   
            //-----------------------------------------------------------------------------------
            //FATURA GUID � OTO OLU�TUR
            modelBuilder.Entity<Fatura>()
                        .Property(p => p.FaturaNo)
                        .HasDefaultValueSql("NEWID()");

            //S�PAR�S � OTO OLU�TUR
            modelBuilder.Entity<Siparis>()
                        .Property(p => p.SiparisNo)
                        .HasDefaultValueSql("NEWID()");

            //fatura
            modelBuilder.Entity<Fatura>()
            .Property(o => o.F_ToplamTutar)
                .HasPrecision(18, 2)
                .IsRequired();

            //odeme
            modelBuilder.Entity<Odeme>()
            .Property(o => o.O_ToplamTutar)
                .HasPrecision(18, 2)
                .IsRequired();

            //siparis durumu
            modelBuilder.Entity<Siparis>()
                .Property(h => h.SiparisDurumu)
                .HasConversion<string>();

            //sepet toplam tutar
            modelBuilder.Entity<Siparis>()
            .Property(s => s.S_ToplamTutar)
                .HasPrecision(18, 2)
                .IsRequired();

            //siparis detay birim fiyat
            modelBuilder.Entity<SiparisDetay>()
            .Property(s => s.BirimFiyat)
                .HasPrecision(18, 2)
                .IsRequired();
            //-----ESK�---------------------------------------------------------------------------
            modelBuilder.Entity<SepetDetay>()
               .Property(s => s.BrutFiyat)
               .HasPrecision(8, 2);

            // �l-�l�e ili�kisi
            modelBuilder.Entity<Ilce>()
                .HasOne(i => i.Il)
                .WithMany(i => i.Ilceler)
                .HasForeignKey(i => i.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres-�l�e ili�kisi
            modelBuilder.Entity<AdresBilgileri>()
                .HasOne(a => a.Ilce)
                .WithMany()
                .HasForeignKey(a => a.IlceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Adres-�l ili�kisi
            modelBuilder.Entity<AdresBilgileri>()
                .HasOne(a => a.Il)
                .WithMany()
                .HasForeignKey(a => a.IlId)
                .OnDelete(DeleteBehavior.Restrict);

            //bir kullan�c�n�n bir adresi olacak �ekilde de�i�tirdim 
            modelBuilder.Entity<HesapBilgileri>()
                 .HasOne(h => h.AdresBilgisi)
                 .WithOne(a => a.HesapBilgileri)
                 .HasForeignKey<AdresBilgileri>(a => a.HesapBilgileriId);

            //C�NS�YET - HESAP DURUMU
            //enumlar�n say�sal de�il metinsel gelmesi icin:
            modelBuilder.Entity<HesapBilgileri>()
                .Property(h => h.CinsiyetTipi)
                .HasConversion<string>();  //Say�sal olsayd� .HasConversion<int>() 

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
            // ISDELETED KISMI BURAYA YAZ
            // IsDeleted == false olanlar� her sorguda otomatik filtrele
            modelBuilder.Entity<Product>()
                        .HasQueryFilter(p => !p.IsDeleted);

            //�r�n fiyat silinenleri getirme
            modelBuilder.Entity<Pricing>()
            .HasQueryFilter(pr => !pr.Product.IsDeleted);
            //ISDELETED KISMI BURAYA YAZ
            //-----------------------------------------------------------
            //-----------------------------------------------------------
            //URUN GUID � OTO OLU�TUR
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
        }
    }
}