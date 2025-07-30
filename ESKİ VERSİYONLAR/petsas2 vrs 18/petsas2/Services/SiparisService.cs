using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class SiparisService: ISiparisService
    {
        private readonly ApplicationDbContext _context;
        public SiparisService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SiparisTamamlaAsync(string kullaniciId, int adresId, string kartIsim, string odemeYontemi)
        {
            // 1. Kullanıcının sepetini getir
            var sepet = await _context.KullaniciSepets
                .Include(s => s.Urunler)
                    .ThenInclude(su => su.UrunID)
                .FirstOrDefaultAsync(sd => sd.KullaniciId == kullaniciId);

            if (sepet == null || !sepet.Urunler.Any())
                return false; // sepet boşsa sipariş oluşturulamaz

            // 2. Yeni sipariş oluştur
            var siparis = new Siparis
            {
                //HesapBilgileriId = HBId,
                AdresId = adresId,
                SiparisTarihi = DateTime.Now,
                KartIsim = kartIsim,
                OdemeYontemi = odemeYontemi,
                SiparisDurumu = SiparisDurumu.SiparisAlindi,
                S_ToplamTutar = 0,
                SiparisDetaylar = new List<SiparisDetay>()
            };

            //// 3. Sepetteki ürünlerden sipariş detaylarını oluştur ve stok düş
            //foreach (var sepetUrun in sepet.Urunler)
            //{
            //    var urun = await _context.Products.FirstOrDefaultAsync(p => p.Id == sepetUrun.UrunID);
            //    if (urun == null)
            //        throw new Exception("Ürün bulunamadı.");

            //    if (urun.Stock < sepetUrun.Adet)
            //        throw new Exception($"Yetersiz stok: {urun.Name}");

            //    // Stoktan düş
            //    urun.Stock -= sepetUrun.Adet;

            //    // Sipariş detayı ekle
            //    siparis.SiparisDetaylar.Add(new SiparisDetay
            //    {
            //        UrunId = urun.Id,
            //        UrunMiktar = sepetUrun.Adet,
            //        BirimFiyat = urun.Pricings.BrutFiyat,
            //    });

            //    // Toplam tutara ekle
            //    siparis.S_ToplamTutar += urun.Fiyat * sepetUrun.Adet;
            //}

            // 4. Siparişi kaydet
            _context.SiparisDb.Add(siparis);

            // 5. Sepeti temizle
            _context.KullaniciSepets.Remove(sepet);

            // 6. Veritabanına tüm değişiklikleri yansıt
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
