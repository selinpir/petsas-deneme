using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using petsas2.Data;
using petsas2.Models;

namespace petsas2.Models
{
    //-------------------------------------------------------------------------------------------------------------------------------------
    //hesap ekle ++
    //hesap sil -
    public class HesapBilgileri
    {
        //Hesap Id
        [Key] public int HBId { get; set; }

        [Required] public string Ad { get; set; } = string.Empty;

        [Required] public string Soyad { get; set; } = string.Empty;

        [Phone]
        [RegularExpression(@"^05\d{9}$", ErrorMessage = "Telefon numarası '05XXXXXXXXX' formatında olmalıdır.")]
        public string Telefon { get; set; } = string.Empty;

        public DateTime DogumTarihi { get; set; }

        public DateTime HesapOlusturmaTarihi { get; set; } = DateTime.UtcNow;

        // Identity ile ilişki (foreign key) application userdan e mail ile kaydolan kullanıcıyı çektik
        [Required] public string UserId { get; set; } = string.Empty;

        //navigation property olan User'ın, hangi FK’ye bağlı olduğunu belirtir
        [ForeignKey("UserId")]
        //navigation property=user
        public ApplicationUser User { get; set; } = null!;
        public ICollection<AdresBilgileri> Adresler { get; set; } = new List<AdresBilgileri>();
        public ICollection<PetBilgileri> PetBilgileris { get; set; } = new List<PetBilgileri>();

        public HesapDurumu HesapDurumu { get; set; } = HesapDurumu.Aktif;
        public Cinsiyet CinsiyetTipi { get; set; } = Cinsiyet.Belirtilmedi;
        //tamamlanmayı false alıyorum kullanıcı hesap bilgilerini ve adres bilgilerini doldurmadan sipariş verilmesine izin vermemesi icin
        public bool IsCompleted { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
    //-------------------------------------------------------------------------------------------------------------------------------------
    //hesabın farklı durumları olabilir bunu tutumak icin enum yapısı kullanılmıştır
    //enum sabit listedir bir değgişkenin belirli değerlerden birini almasını sağlar
    public enum HesapDurumu
    {
        Aktif = 0,
        KullaniciKapatti = 1,
        AdminSildi = 2
    }
    //cinsiyet de enum olarak tutuldu, ilk başta belirtilmedi olarak atandı
    public enum Cinsiyet
    {
        Belirtilmedi = 0,
        Kadın = 1,
        Erkek = 2,
        Diger = 3
    }
    //-------------------------------------------------------------------------------------------------------------------------------------

    public class AdresBilgileri
    {
        [Key]
        public int ABId { get; set; }
        //ilişkiler
        //kullanıcı (bir adres bir kullanıcıya aittir)
        [Required]
        public string UserId { get; set; } = null!; // string çünkü IdentityUser.Id string
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;
        //il-ilçe
        [Required] public int IlId { get; set; }
        [ForeignKey("IlId")] public Il Il { get; set; } = null!;
        [Required] public int IlceId { get; set; }
        [ForeignKey("IlceId")] public Ilce Ilce { get; set; } = null!;
        public string AcikAdres { get; set; } = string.Empty;
        public string AdresAdi { get; set; } = string.Empty;
        //Adresin silinip silinmediği
        public bool IsDeleted { get; set; } = false;
        //tamamlanmayı false alıyorum kullanıcı hesap bilgilerini ve adres bilgilerini doldurmadan sipariş verilmesine izin vermemesi icin
        public bool IsCompleted { get; set; } = false;
    }
    public class Il
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public ICollection<Ilce> Ilceler { get; set; } = new List<Ilce>();
    }

    public class Ilce
    {
        public int Id { get; set; }
        public int IlId { get; set; }
        public string Ad { get; set; }
        public Il Il { get; set; }
    }

    //-------------------------------------------------------------------------------------------------------------------------------------


    //PET BİLGİLERİ
    //bir pet bir kategoriye aittir. bir kategoride birden fazla pet olabilir.
    //ayrıca bir kullanıcının birden çok pet bilgisi olabilir.
    public class PetBilgileri
    {
        [Key]
        public int PBId { get; set; }

        //ilişkiler
        //kullanıcı (bir pet bir kullanıcıya aittir)
        [Required]
        public int HesapBilgileriId { get; set; }
        public HesapBilgileri HesapBilgileri { get; set; } = null!;

        //kategori (bir pet bir kategoriye aittir)
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        //ilişkiler

        [Required]
        public string PetAdi { get; set; } = string.Empty;
        public string? GorselUrl { get; set; } = string.Empty;

        //dg bos geçilebilir
        public DateTime? PetDogum { get; set; }
        //ek metin bos geçilebilir
        public string? EkMetin { get; set; } = string.Empty;

        //petin silinmesi durumunda
        public bool IsDeleted { get; set; } = false;
    }
}
//PET BİLGİLERİ



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

//HesapBilgileri BİLGİİLERİM EK
//kredi kartı
//kredi kart tipi ıd
//kart son ay
//kart son yıl
//fatura ve adres bir sayılacak