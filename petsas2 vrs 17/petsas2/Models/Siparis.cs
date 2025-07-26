using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using petsas2.Areas.User;

namespace petsas2.Models
{
    public class Siparis
    {
    }
}
//public class Siparis
//{
//    public int Id { get; set; }
//    public string KullaniciId { get; set; }
//    public DateTime SiparisTarihi { get; set; }
//    public string SiparisDurumu { get; set; } // Örn: Hazırlanıyor, Kargoya Verildi, Teslim Edildi vb.

//    public int AdresId { get; set; }
//    public Adres Adres { get; set; }

//    public List<SiparisDetay> SiparisDetaylari { get; set; } = new();
//}

//public class SiparisDetay
//{
//    public int Id { get; set; }
//    public int SiparisId { get; set; }
//    public Siparis Siparis { get; set; }

//    public int UrunId { get; set; }
//    public string UrunAd { get; set; }
//    public decimal Fiyat { get; set; }
//    public int Adet { get; set; }
//}


//public async Task<bool> SiparisTamamlaAsync(string kullaniciId, int adresId)
//{
//    var kullaniciSepet = await _context.KullaniciSepets
//        .Include(s => s.Urunler)
//        .FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);

//    if (kullaniciSepet == null || !kullaniciSepet.Urunler.Any())
//        return false;

//    var yeniSiparis = new Siparis
//    {
//        KullaniciId = kullaniciId,
//        SiparisTarihi = DateTime.Now,
//        SiparisDurumu = "Hazırlanıyor",
//        AdresId = adresId,
//        SiparisDetaylari = new List<SiparisDetay>()
//    };

//    foreach (var urun in kullaniciSepet.Urunler)
//    {
//        var urunDb = await _context.Products.FirstOrDefaultAsync(p => p.Id == urun.UrunID);
//        if (urunDb == null || !urunDb.AktifMi)
//            return false; // Ürün bulunamadı veya aktif değil

//        if (urunDb.Stok < urun.Adet)
//            return false; // Yeterli stok yok

//        // Stok azalt
//        urunDb.Stok -= urun.Adet;
//        if (urunDb.Stok < urunDb.MinStok)
//            urunDb.AktifMi = false; // Satıştan kaldır

//        yeniSiparis.SiparisDetaylari.Add(new SiparisDetay
//        {
//            UrunId = urun.UrunID,
//            UrunAd = urun.UrunAd,
//            Fiyat = urun.BrutFiyat,
//            Adet = urun.Adet
//        });
//    }

//    _context.Siparisler.Add(yeniSiparis);

//    // Sepeti temizle veya işaretle
//    _context.KullaniciSepets.Remove(kullaniciSepet);

//    await _context.SaveChangesAsync();
//    return true;
//}

//5.Siparişler(Orders)
//CREATE TABLE orders (
//    order_id SERIAL PRIMARY KEY,
//    user_id INT REFERENCES users(user_id),
//    shipping_address_id INT REFERENCES addresses(address_id),
//    billing_address_id INT REFERENCES addresses(address_id),
//    status VARCHAR(20), -- örn: pending, shipped, completed
//    total_amount NUMERIC(10,2),
//    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
//);
//Sipariş Ürünleri(Order Items)
//CREATE TABLE order_items (
//    order_item_id SERIAL PRIMARY KEY,
//    order_id INT REFERENCES orders(order_id),
//    product_id INT REFERENCES products(product_id),
//    quantity INT,
//    price NUMERIC(10,2)
//);

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