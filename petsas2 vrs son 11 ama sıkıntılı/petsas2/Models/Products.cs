using Humanizer; //bu vardı galiba ben eklemedim ? sayıları vs insanın anlayacagi sekile getirmekte
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System.Collections.Generic;

namespace petsas2.Models
{   //---------------------------------------------------------------------
    //category: ust kategori : kedi-kopek-kus-balik-tavsan-hamster --defter--
    //subcategory : ust kategorinin alt kategorisi- kedi kumu- kopek oyuncagı  --defter--
    //product : urunler sınıfı --defter--
    //brand : marka sınıfı --defter--
    //stockalertdto
    //SPrdctListDto
    //---------------------------------------------------------------------
    //CRUD YAPILANLAR
    //brandManagement++
    //categoryManagement++
    //subcategoryManagement++
    //productManagement++
    //stockAlertDto++
    //CRUD YAPILANLAR
    //---------------------------------------------------------------------
    //PetType: kedi,köpek,kuş,akvaryum,tavşan,hamster
    public class Category
    {
        public int Id { get; set; }
        public string PetType { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        //Bir kategorinin birden çok alt kategorisi olabileceği belirtilmiştir.

        public ICollection<PetBilgileri> PetBilgileris { get; set; } = new List<PetBilgileri>();
        //bir kategorinin birden çok peti olabilir
        //yeni eklendi-kullanici ile birlikte       
    }

    //ProductName:petler icin alt kategori ürünler fln mama,tasma
    public class SubCategory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty; //mama
        public int CategoryId { get; set; } //yavru kedi maması
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = new List<Product>();
        // Bir alt kategoriye ait birden çok ürün olabileceği belirtilmiştir.
    }


    public class Product
    {
        public int Id { get; set; }
        //Soft delete yaklasimi urun tamamen silinmez gorunumden kaldırılır ama vt durmaya devam eder.
        public bool IsDeleted { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //Globally Unique Identifier - Barkodların birbirinden farklı olması ve otomatik atanması için GUID kullanılmıştır.
        public Guid? Barcode { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int MinStock { get; set; } = 0;
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? Color { get; set; }
        public decimal? Weight { get; set; }  
        public string? Dimensions { get; set; }      //boyut olarak    
        public string? Size { get; set; }    //beden olarak
        public string? Material { get; set; }  
        public ICollection<Pricing> Pricings { get; set; } = new List<Pricing>();
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Brand : Bu sınıf, ürünlerin bağlı olduğu markayı temsil etmektedir.
    // İlişki olarak bir ürünün bir markası olabilir, bir marka birden fazla ürüne ait olabilir ilişkisi belirlendi.
    // Marka silme işleminde soft delete yaklaşımı uygulandı.
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

   
    public class StockAlertDto
    {
        public int ProductId { get; set; }
        public Guid Barcode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int MinStock { get; set; }
    }

    public class SPrdctListDto
    {
        public int ProductId { get; set; }
        public Guid Barcode { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int MinStock { get; set; }
    }

}
