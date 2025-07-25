using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

//SCD TYPE 2: değişen boyut değerleri için yeni bir satır eklenir ve geçerli olan eski kayıtlar korunur. 
namespace petsas2.Models
{
    public class Pricing
    {
        public int FiyatId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal NetFiyat { get; set; } //net fiyat: urun satılmadan önceki fiyat
        public decimal? IndirimOrani { get; set; }
        public decimal? KdvOrani { get; set; }
        //eski fiyatları da tutabilmek icin scd slowly changing dimension yapısı varmıs o kullanıldı
        public DateTime FiyatBaslangicTarihi { get; set; }
        public DateTime? FiyatSonlanmaTarihi { get; set; }
            
        //net fiyat* (indirim oranı)
        [NotMapped]
        public decimal IndirimTutari
            => NetFiyat * (IndirimOrani ?? 0m);
        //0m: indirim yoksa 0 varsay

        //indirimli fiyat = net-indirim tutarı
        [NotMapped]
        public decimal IndirimliFiyat
            => NetFiyat - IndirimTutari;

        //Kdvtutarı= indirimli f*(kdv oranı)
        [NotMapped]
        public decimal KdvTutari
            => IndirimliFiyat * (KdvOrani ?? 0m);

        //brut= ind f+ kdv tutarı
        [NotMapped]
        public decimal BrutFiyat
            => IndirimliFiyat + KdvTutari;

    }
}
