using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class SiparisService : ISiparisService
    {
        private readonly ApplicationDbContext _context;
        public SiparisService(ApplicationDbContext context)
        {
            _context = context;
        }
        //-----------------------------------------------------------------------------------------------------------------------
        public async Task<bool> SiparisTamamlaAsync(string kullaniciId, int adresId, string kartIsim, string odemeYontemi)
        {
            var sepet = await _context.KullaniciSepets
                .Include(s => s.Urunler)
                .FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);



            if (sepet == null || !sepet.Urunler.Any())
                return false;

            var siparis = new Siparis
            {
                HesapBilgileriId = int.Parse(kullaniciId), // veya uygun dönüşüm
                AdresId = adresId,
                SiparisTarihi = DateTime.Now,
                KartIsim = kartIsim,
                OdemeYontemi = odemeYontemi,
                SiparisDurumu = SiparisDurumu.SiparisAlindi,
                S_ToplamTutar = sepet.SepetToplamTutar,
                SiparisDetaylar = new List<SiparisDetay>()
            };

            foreach (var urunDetay in sepet.Urunler)
            {
                var product = await _context.Products
                    .Include(p => p.Pricings)
                    .FirstOrDefaultAsync(p => p.Id == urunDetay.UrunID);

                if (product == null)
                    throw new Exception("Ürün bulunamadı");

                var aktifFiyat = product.Pricings
                    .Where(f => f.FiyatSonlanmaTarihi == null || f.FiyatSonlanmaTarihi > DateTime.Now)
                    .OrderByDescending(f => f.FiyatBaslangicTarihi)
                    .FirstOrDefault();

                if (aktifFiyat == null)
                    throw new Exception("Ürünün aktif fiyatı bulunamadı");

                var brutFiyat = aktifFiyat.BrutFiyat;

                if (product.Stock < urunDetay.Adet)
                    throw new Exception($"Yetersiz stok: {product.Name}");

                product.Stock -= urunDetay.Adet;

                siparis.SiparisDetaylar.Add(new SiparisDetay
                {
                    UrunId = product.Id,
                    UrunMiktar = urunDetay.Adet,
                    BirimFiyat = brutFiyat
                });
            }

            siparis.Odeme = new Odeme
            {
                OdemeTarihi = DateTime.Now,
                O_ToplamTutar = siparis.S_ToplamTutar,
                KartIsim = kartIsim,
                OdemeYontemi = odemeYontemi
            };

            siparis.Fatura = new Fatura
            {
                FaturaTarihi = DateTime.Now,
                MusteriAdi = kartIsim,
                F_ToplamTutar = siparis.S_ToplamTutar,
                SiparisNo = siparis.SiparisNo,
                OdemeYontemi = odemeYontemi,
                SaticiUnvan = "Petsas Pet Market",
                SaticiAdSoyad = "Selin Pir",
                SaticiAdres = "İzmir, Türkiye",
                SaticiVergiNo = "1234567890"
            };

            _context.SiparisDb.Add(siparis);
            _context.KullaniciSepets.Remove(sepet);
            await _context.SaveChangesAsync();

            return true;
        }
        //-----------------------------------------------------------------------------------------------------------------------
        //
        public async Task<List<Siparis>> GetKullaniciSiparisleriAsync(int hesapBilgileriId)
        {
            return await _context.SiparisDb
                .Where(s => s.HesapBilgileriId == hesapBilgileriId)
                .Include(s => s.SiparisDetaylar)
                    .ThenInclude(d => d.Urun)
                .Include(s => s.Adres)
                .Include(s => s.Odeme)
                .Include(s => s.Fatura)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }
        //-----------------------------------------------------------------------------------------------------------------------
        // SİPARİS DURUMUNU GUNCELLEMEK-hazırlanıyor-kargo-teslim edildi vs
        public async Task<bool> SiparisDurumunuGuncelleAsync(int siparisId, SiparisDurumu yeniDurum)
        {
            var siparis = await _context.SiparisDb.FindAsync(siparisId);
            if (siparis == null)
                return false;

            siparis.SiparisDurumu = yeniDurum;
            await _context.SaveChangesAsync();
            return true;
        }
        //-----------------------------------------------------------------------------------------------------------------------

    }


}
