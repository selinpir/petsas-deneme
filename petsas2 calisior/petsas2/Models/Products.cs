using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace petsas2.Models
{
    //Category - SubCategory - Product 
    //PetType: kedi,köpek,kuş,akvaryum,tavşan,hamster
    public class Category
    {
        public int Id { get; set; }
        public string PetType { get; set; } = string.Empty;
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }

    //ProductName:petler icin alt kategori ürünler fln mama,tasma
    public class SubCategory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
    //urunler
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //Globally Unique Identifier - barkodlar essiz olsun diye
        public Guid? Barcode { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int MinStock { get; set; } = 0;
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int BrandId { get; set; } //markalar için
        public Brand? Brand { get; set; }
        public decimal? DiscountedRatio { get; set; } // İndirim oranı
        public decimal? DiscountedPrice { get; set; } // İndirimli fiyat 
        public decimal? KdvRatio { get; set; } // kdv oranı
        public decimal? KdvPrice { get; set; } // kdvli fiyat
        public decimal? Rating { get; set; } = 0; // begeni
        public ICollection<ProductFeature> Features { get; set; } = new List<ProductFeature>();
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }


    //urun ozelligi
    public class ProductFeature
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string Name { get; set; } = string.Empty; // Renk, Boyut, Malzeme
        public string Value { get; set; } = string.Empty; //  Kırmızı, 10kg, Plastik

    }


}
