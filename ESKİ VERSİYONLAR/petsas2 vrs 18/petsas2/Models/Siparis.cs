using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using petsas2.Areas.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsas2.Models
{
    //------------------------------------------------------------------------------------------------------
    public class Siparis //+
    {
        public int SiparisID { get; set; }
        public Guid SiparisNo { get; set; } = Guid.NewGuid();
        public DateTime SiparisTarihi { get; set; } = DateTime.Now;
        public decimal S_ToplamTutar { get; set; } //hp +
        public SiparisDurumu SiparisDurumu { get; set; } = SiparisDurumu.SiparisAlindi;
        public string KartIsim { get; set; } //ad-soyad
        public string OdemeYontemi { get; set; } = "Kredi Kartı";
        //--
        // fk - Kullanıcı
        [Required] public int HesapBilgileriId { get; set; }
        [ForeignKey("HBId")] public HesapBilgileri HesapBilgileri { get; set; }
        //--
        // fk- Adres
        [Required] public int AdresId { get; set; }
        [ForeignKey("ABId")] public AdresBilgileri Adres { get; set; }
        //--
        //siparişe ait ürün satırları
        public List<SiparisDetay> SiparisDetaylar { get; set; } = new();      
        public Odeme Odeme { get; set; }
        public Fatura Fatura { get; set; }
    }
    public enum SiparisDurumu //+
    {
        SiparisAlindi = 0,
        SiparisHazirlaniyor = 1,
        SiparisKargoyaVerildi = 2,
        SiparisTeslimEdildi = 3
    }
    //------------------------------------------------------------------------------------------------------
    public class SiparisDetay
    {
        public int SiparisDetayId { get; set; }
        // fk - Siparis
        [Required] public int SiparisID { get; set; }
        [ForeignKey("SiparisID")] public Siparis? Siparis { get; set; }
        // fk - Urun
        [Required] public int UrunId { get; set; }
        [ForeignKey("ProductId")] public Product? Urun { get; set; }
        public int UrunMiktar { get; set; }
        public decimal BirimFiyat { get; set; } // sipariş anındaki fiyat //hp+
    }
    //------------------------------------------------------------------------------------------------------
    public class Odeme
    {
        public int OdemeId { get; set; }
        public DateTime OdemeTarihi { get; set; } = DateTime.Now;
        public decimal O_ToplamTutar { get; set; } //hp+
        // ad-soyad
        public string KartIsim { get; set; } = string.Empty;
        public string OdemeYontemi { get; set; } = "Kredi Kartı";
        // sipariş ile ilişki (1:1) bir siparis bir odeme
        public int SiparisID { get; set; }
        public Siparis? Siparis { get; set; }
    }
    public class Fatura
    {
        public int FaturaId { get; set; }
        public DateTime FaturaTarihi { get; set; } = DateTime.Now;
        public string MusteriAdi { get; set; } = string.Empty;
        public decimal F_ToplamTutar { get; set; } //hp -
        public Guid FaturaNo { get; set; } = Guid.NewGuid();
        public Guid SiparisNo { get; set; }
        public string OdemeYontemi { get; set; } = "Kredi Kartı";
        // bizim bilgiler
        public string SaticiUnvan { get; set; } = "Petsas Pet Market";
        public string SaticiAdSoyad { get; set; } = "Selin Pir";
        public string SaticiAdres { get; set; } = "İzmir, Türkiye";
        public string SaticiVergiNo { get; set; } = "1234567890";
        // Sipariş ilişkisi
        public int SiparisID { get; set; }
        public Siparis? Siparis { get; set; }
        // sipariş detayları 
        public List<SiparisDetay> SiparisDetaylari { get; set; } = new();
    }
}
