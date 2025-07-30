using System.ComponentModel.DataAnnotations.Schema;

namespace petsas2.Models
{
    //--------------------------------------------------------------------------------------------------------
    public class SepetDetay
    {
        public int Id { get; set; } // Primary key
        public int UrunID { get; set; }
        public string UrunAd { get; set; }
        public string Gorsel { get; set; }
        public decimal BrutFiyat { get; set; }
        public int Adet { get; set; }
        [NotMapped]
        public decimal ToplamTutar => BrutFiyat * Adet;
        public int KullaniciSepetId { get; set; } // Foreign key
        public KullaniciSepet KullaniciSepet { get; set; }
    }
    //--------------------------------------------------------------------------------------------------------
    public class KullaniciSepet
    {
        public int Id { get; set; }
        public string KullaniciId { get; set; }
        public int SepetId { get; set; }       
        public List<SepetDetay> Urunler { get; set; } = new();
        [NotMapped]
        public decimal SepetToplamTutar => Urunler.Sum(i => i.ToplamTutar);
    }
    //--------------------------------------------------------------------------------------------------------
}