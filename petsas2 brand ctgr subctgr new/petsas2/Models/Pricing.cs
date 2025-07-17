using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


//SCD TYPE 2: değişen boyut değerleri için yeni bir satır eklenir ve geçerli olan eski kayıtlar korunur. 
namespace petsas2.Models
{
    //ürünü fiyatlandırma
    public class Pricing
    {
        public int FiyatId { get; set; }
        public int ProductId { get; set; }
        //navigation property ürün ıd almak icin
        public Product Product { get; set; } = null!;

        //net fiyat: urun satılmadan önceki fiyat
        public decimal NetFiyat { get; set; }

        public decimal? IndirimOrani { get; set; }
        public decimal KdvOrani { get; set; }
        //eski fiyatları da tutabilmek icin scd slowly changing dimension yapısı varmıs o kullanıldı
        public DateTime FiyatBaslangicTarihi { get; set; }
        public DateTime? FiyatSonlanmaTarihi { get; set; }

        //sadece c# ksımında hesaplıcaz db geçmicek

        [NotMapped]
        public decimal IndirimTutari
            => NetFiyat * (IndirimOrani ?? 0m);
        //0m: indirim yoksa 0 varsay

        [NotMapped]
        public decimal IndirimliFiyat
            => NetFiyat - IndirimTutari;

        [NotMapped]
        public decimal KdvTutari
            => IndirimliFiyat * KdvOrani;

        [NotMapped]
        public decimal BrutFiyat
            => IndirimliFiyat + KdvTutari;

    }
}
