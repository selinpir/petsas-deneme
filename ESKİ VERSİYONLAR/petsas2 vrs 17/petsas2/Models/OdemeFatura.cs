

using petsas2.Models;

namespace petsas2.Models
{
    public class OdemeFatura
    {
    }
}

//Ödeme Durumları(Payments)
//CREATE TABLE payments (
//    payment_id SERIAL PRIMARY KEY,
//    order_id INT REFERENCES orders(order_id),
//    method VARCHAR(30), -- örn: credit_card, PayPal, bank_transfer
//    status VARCHAR(20), -- örn: pending, paid, failed, refunded
//    paid_at TIMESTAMP,
//    amount NUMERIC(10,2)
//);

//6.Faturalar(Invoices)
//CREATE TABLE invoices (
//    invoice_id SERIAL PRIMARY KEY,
//    order_id INT REFERENCES orders(order_id),
//    issued_date DATE,
//    total_amount NUMERIC(10,2)
//);

//public class IndirimPolitikasi
//{
//    public int Id { get; set; }

//    // Örnek: “Yaz İndirimi”
//    public string Ad { get; set; } = string.Empty;

//    // 0–1 arası oran, örn. 0.10m = %10
//    public decimal Oran { get; set; }

//    public DateTime? BaslangicTarihi { get; set; }
//    public DateTime? BitisTarihi { get; set; }

//    // Verilen tarihte indirim politikasının geçerli olup olmadığını kontrol eder
//    public bool GecerliMi(DateTime tarih)
//        => (!BaslangicTarihi.HasValue || tarih >= BaslangicTarihi)
//        && (!BitisTarihi.HasValue || tarih <= BitisTarihi);
//}

//public class Fatura
//{
//    public int Id { get; set; }
//    public DateTime Tarih { get; set; }

//    public string MusteriAdi { get; set; } = string.Empty;
//    public string MusteriVergiNo { get; set; } = string.Empty;

//    public ICollection<FaturaSatiri> Satirlar { get; set; }
//        = new List<FaturaSatiri>();

//    // Toplam net tutar (indirim ve KDV öncesi)
//    public decimal ToplamNet
//        => Satirlar.Sum(s => s.NetTutar);

//    // Toplam indirim tutarı
//    public decimal ToplamIndirim
//        => Satirlar.Sum(s => s.IndirimTutari);

//    // Toplam KDV tutarı
//    public decimal ToplamKdv
//        => Satirlar.Sum(s => s.KdvTutari);

//    // Toplam brüt tutar (indirim ve KDV sonrası)
//    public decimal ToplamBrut
//        => Satirlar.Sum(s => s.BrutTutar);
//}


//public class FaturaSatiri
//{
//    public int Id { get; set; }

//    public int UrunId { get; set; }
//    public Product? Urun { get; set; }

//    public int Miktar { get; set; }

//    // Birim net fiyat (örneğin Product.BasePrice)
//    public decimal BirimNetFiyat { get; set; }

//    // Satır için uygulanan indirim oranı (0–1 arası)
//    public decimal IndirimOrani { get; set; }

//    // İndirim tutarı = BirimNetFiyat × IndirimOrani × Miktar
//    public decimal IndirimTutari
//        => BirimNetFiyat * IndirimOrani * Miktar;

//    // İndirimsiz net tutar = BirimNetFiyat × Miktar
//    // NetTutar = İndirimsiz net tutar – IndirimTutari
//    public decimal NetTutar
//        => (BirimNetFiyat * Miktar) - IndirimTutari;

//    // KDV oranı (örneğin Product.DefaultVatRate)
//    public decimal KdvOrani { get; set; }

//    // KDV tutarı = NetTutar × KdvOrani
//    public decimal KdvTutari
//        => NetTutar * KdvOrani;

//    // Brüt tutar = NetTutar + KdvTutari
//    public decimal BrutTutar
//        => NetTutar + KdvTutari;
//}

//public class FaturaSatiri
//{
//    public int Id { get; set; }
//    public int UrunId { get; set; }
//    public Product? Urun { get; set; }
//    public int Miktar { get; set; }

//    // Ürünün net fiyatını alıp, satırdaki KDV oranını buraya geçir
//    public decimal BirimNetFiyat { get; set; }
//    public decimal IndirimOrani { get; set; }
//    public decimal KdvOrani { get; set; }

//    // Hesaplamayı FiyatDetay ile yap
//    public FiyatDetay Detay => new()
//    {
//        NetFiyat = BirimNetFiyat,
//        IndirimOrani = IndirimOrani,
//        KdvOrani = KdvOrani
//    };

//    public decimal NetTutar => Detay.IndirimliFiyat * Miktar;
//    public decimal KdvTutari => Detay.KdvTutari * Miktar;
//    public decimal BrutTutar => Detay.BrutFiyat * Miktar;
//}
//public class FiyatDetay
//{
//    public decimal NetFiyat { get; init; }
//    public decimal IndirimOrani { get; init; }
//    public decimal KdvOrani { get; init; }

//    public decimal IndirimliFiyat => NetFiyat * (1 - IndirimOrani);
//    public decimal KdvTutari => IndirimliFiyat * KdvOrani;
//    public decimal BrutFiyat => IndirimliFiyat + KdvTutari;
//}
