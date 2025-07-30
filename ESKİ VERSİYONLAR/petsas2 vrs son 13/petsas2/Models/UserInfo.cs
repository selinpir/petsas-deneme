using petsas2.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace petsas2.Models
{
    //--HESAPBİLGİLERİ----------------------------------------------------------------------------------------------
    public class HesapBilgileri
    {
        [Key] public int HBId { get; set; }
        [Required] public string UserId { get; set; } = string.Empty;

        //navigation property olan User'ın, hangi FK’ye bağlı olduğunu belirtir
        [ForeignKey("UserId")]
        //navigation property=user
        public ApplicationUser User { get; set; } = null!;

        [Required] public string Ad { get; set; } = string.Empty;

        [Required] public string Soyad { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;

        public DateTime DogumTarihi { get; set; }

        public DateTime HesapOlusturmaTarihi { get; set; } = DateTime.UtcNow;

        public HesapDurumu HesapDurumu { get; set; } = HesapDurumu.Aktif;
        public Cinsiyet CinsiyetTipi { get; set; } = Cinsiyet.Belirtilmedi;
        public bool IsCompleted { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        //hesap silmeyi ve adminin hesap silmesini ileriye attım şuanda zamanım yok 
        public ICollection<PetBilgileri> PBilgileri { get; set; } = new List<PetBilgileri>();
        //daha sonra eklenecek
        //public ICollection<AdresBilgileriTekrar> Adresler { get; set; } = new List<AdresBilgileriTekrar>();

        //daha sonra eklenecek
    }
    public enum HesapDurumu
    {
        Aktif = 0,
        KullaniciSildi = 1,
        AdminSildi = 2
    }

    public enum Cinsiyet
    {
        Belirtilmedi = 0,
        Kadın = 1,
        Erkek = 2,
        Diger = 3
    }
    //--HESAPBİLGİLERİ----------------------------------------------------------------------------------------------
    //--PETBİLGİLERİ------------------------------------------------------------------------------------------------
    public class PetBilgileri
    {
        [Key] public int PBId { get; set; }
        [Required] public string PetAdi { get; set; } = string.Empty;
        public string? GorselUrl { get; set; } = string.Empty;
        public DateTime? PetDogum { get; set; }
        public string? EkMetin { get; set; } = string.Empty;

        //kullanıcı (bir pet bir kullanıcıya aittir)
        [Required]
        public int HesapBilgileriId { get; set; }
        public HesapBilgileri HesapBilgileri { get; set; } = null!;

        //kategori (bir pet bir kategoriye aittir)
        [Required] public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;           
        public bool IsDeleted { get; set; } = false;
    }
    //--PETBİLGİLERİ----------------------------------------------------------------------------------------------




    public class AdresBilgileri
    {
        //adres id
        //bir kullanıcının birden fazla adresi olabilir
        //uyeid
        //adres adı
        //il seç
        //ilçe seç
        //açık adres
    }


}

//Siparişler Tablosu
//- order_id (Birincil Anahtar)
//- user_id (Kullanıcı Hesaplarına referans veren Yabancı Anahtar)
//- ödeme yöntemi
//- Shipping_address_id (Yabancı Anahtar referans Adresi)
//- billing_address_id (Yabancı Anahtar referans Adresi)
//- Sipariş durumu
//- sipariş tarihi
//- toplam tutar

//Sipariş Kalemleri Tablosu: Bu tablo, her siparişte
//yer alan ürünleri ve satın alma anındaki miktar ve fiyatını kaydeder.
//- order_item_id (Birincil Anahtar)
//- order_id (Yabancı Anahtar referanslı Siparişler)
//- product_id (ProductCatalog'a referans veren Yabancı Anahtar)
//- miktar
//- fiyat_at_purchase