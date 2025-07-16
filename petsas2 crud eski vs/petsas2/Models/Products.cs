using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//Category - SubCategory - ProductNew - ProductFeature - Brand
namespace petsas2.Models
{
    //brandManagement++
    //categoryManagement++
    //subcategoryManagement++
    //ProductNewManagement-
    //productFeatureManagement-

    //PetType: kedi,köpek,kuş,akvaryum,tavşan,hamster
    public class Category
    {
        public int Id { get; set; }
        public string PetType { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        //bir kategorinin birden çok alt kategorisi olabilir
    }

    //ProductName:petler icin alt kategori ürünler fln mama,tasma
    public class SubCategory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty; //mama
        public int CategoryId { get; set; } //yavru kedi % maması
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<ProductNew> ProductNews { get; set; } = new List<ProductNew>();
    }
    public class ProductNew
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Barkod otomatik SQL/EF Core ile üretilir
        public Guid Barcode { get; set; }

        // Stok takibi
        public int Stock { get; set; }
        public int MinStock { get; set; } = 0;

        // İlişkiler
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        // Temel fiyat ve vergi oranı
        public decimal NetBirimFiyat { get; set; }            // Net birim fiyat
        public decimal DefaultKdvOranı { get; set; }       // Örn. 0.18m

        // Ürün özellikleri
        public ICollection<ProductFeature> Features { get; set; }
            = new List<ProductFeature>();
    }


    //urunler
    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } = string.Empty;
    //    public string Description { get; set; } = string.Empty;
    //    //Globally Unique Identifier - barkodlar essiz olsun diye
    //    public Guid? Barcode { get; set; }
    //    public decimal Price { get; set; }
    //    public string ImageUrl { get; set; } = string.Empty;
    //    public int Stock { get; set; }
    //    public int MinStock { get; set; } = 0;
    //    public int SubCategoryId { get; set; }
    //    public SubCategory? SubCategory { get; set; }
    //    public int BrandId { get; set; } //markalar için
    //    public Brand? Brand { get; set; }
    //    public decimal? DiscountedRatio { get; set; } // İndirim oranı
    //    public decimal? DiscountedPrice { get; set; } // İndirimli fiyat 
    //    public decimal? KdvRatio { get; set; } // kdv oranı
    //    public decimal? KdvPrice { get; set; } // kdvli fiyat
    //    public decimal? Rating { get; set; } = 0; // begeni
    //    public ICollection<ProductFeature> Features { get; set; } = new List<ProductFeature>();
    //}

    //urunun markası- her urunun bir markası olur- bir markanın birden çok urunu olabilir
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //soft delete- tamamen silmek yerine görünümden kaldırmak ama birdaha kullanılmayacak zaten marka çöktüyse falan
        public bool IsDeleted { get; set; } = false;
        public ICollection<ProductNew> ProductNews { get; set; } = new List<ProductNew>();     
        //bir marka birden çok ürün ile ilişkili olabilir.
    }


    //urun ozelligi
    public class ProductFeature
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductNew? ProductNew { get; set; }
        public string Name { get; set; } = string.Empty; // Renk, Boyut, Malzeme
        public string Value { get; set; } = string.Empty; //  Kırmızı, 10kg, Plastik

    }
}
