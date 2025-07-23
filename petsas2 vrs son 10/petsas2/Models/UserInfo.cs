using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using petsas2.Data;
using petsas2.Models;

namespace petsas2.Models
{
    public class HesapBilgileri
    {
        //Hesap Id
        [Key]
        public int HBId { get; set; }

        [Required]
        public string Ad { get; set; } = string.Empty;

        [Required]
        public string Soyad { get; set; } = string.Empty;

        [Phone]
        [RegularExpression(@"^05\d{9}$", ErrorMessage = "Telefon numarası '05XXXXXXXXX' formatında olmalıdır.")]
        public string Telefon { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [CustomValidation(typeof(HesapBilgileri), nameof(DogumTarihiKontrol))]
        public DateTime DogumTarihi { get; set; }

        public DateTime HesapOlusturmaTarihi { get; set; } = DateTime.UtcNow;

        // Identity ile ilişki (foreign key) application userdan e mail ile kaydolan kullanıcıyı çektik
        [Required]
        public string UserId { get; set; } = string.Empty;

        //navigation property olan User'ın, hangi FK’ye bağlı olduğunu belirtir
        [ForeignKey("UserId")]

        //navigation property=user
        public ApplicationUser User { get; set; } = null!;

        public ICollection<AdresBilgileri> Adresler { get; set; } = new List<AdresBilgileri>();
        public ICollection<PetBilgileri> PetBilgileris { get; set; } = new List<PetBilgileri>();

        public static ValidationResult? DogumTarihiKontrol(DateTime dogumTarihi, ValidationContext context)
        {
            if (dogumTarihi > DateTime.Today)
                return new ValidationResult("Doğum tarihi ileri tarih olamaz.");

            int yas = DateTime.Today.Year - dogumTarihi.Year;
            if (dogumTarihi > DateTime.Today.AddYears(-yas)) yas--;

            return yas >= 18
                ? ValidationResult.Success
                : new ValidationResult("Kullanıcı en az 18 yaşında olmalıdır.");
        }
        public HesapDurumu HesapDurumu { get; set; } = HesapDurumu.Aktif;
        public Cinsiyet CinsiyetTipi { get; set; } = Cinsiyet.Belirtilmedi;
        //tamamlanmayı false alıyorum kullanıcı hesap bilgilerini ve adres bilgilerini doldurmadan sipariş verilmesine izin vermemesi icin
        public bool IsCompleted { get; set; } = false;

    }

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

    public class AdresBilgileri
    {
        [Key]
        public int ABId { get; set; }
        //ilişkiler
        //kullanıcı (bir adres bir kullanıcıya aittir)
        [Required]
        public int HesapBilgileriId { get; set; }

        [ForeignKey("HesapBilgileriId")]
        public HesapBilgileri HesapBilgileri { get; set; } = null!;

        // İl ve İlçe ilişkileri
        public int id { get; set; }
        public il il { get; set; } = null!;

        public int il_id { get; set; }
        public ilce ilce { get; set; } = null!;
        //ilişkiler
        public string AcikAdres { get; set; } = string.Empty;
        //Adresin silinip silinmediği
        public bool IsDeleted { get; set; } = false;
        //tamamlanmayı false alıyorum kullanıcı hesap bilgilerini ve adres bilgilerini doldurmadan sipariş verilmesine izin vermemesi icin
        public bool IsCompleted { get; set; } = false;
    }
    //IL-ILCE
    //il ve ilce sql üzerinden eklenmişti
    public class il
    {
        public int id { get; set; }
        public string ad { get; set; }

        public ICollection<ilce> Ilceler { get; set; } = new List<ilce>();
    }
        public class ilce
    {
        public int id { get; set; }
        public int il_id { get; set; }
        public string ad { get; set; }
        public il il { get; set; } = null!;
    }
    //IL-ILCE

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
    //PET BİLGİLERİ

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

//HesapBilgileri BİLGİİLERİM EK
//kredi kartı
//kredi kart tipi ıd
//kart son ay
//kart son yıl
//fatura ve adres bir sayılacak