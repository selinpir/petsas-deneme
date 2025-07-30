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
        //----------------------------------------------------------------------------------------------------------------------
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

        //----------------------tek siparis-------------------------------------------------------------------------------------

        public async Task<Siparis> GetSiparisByIdAsync(int siparisId)
        {
            return await _context.SiparisDb
                 .Include(s => s.SiparisDetaylar)
                     .ThenInclude(d => d.Urun)
                 .Include(s => s.Adres)
                 .Include(s => s.Odeme)
                 .Include(s => s.Fatura)
                 .FirstOrDefaultAsync(s => s.SiparisID == siparisId);
        }

        //----------------------------------------------------------------------------------------------------------------------
        public async Task<bool> SiparisDurumunuGuncelleAsync(int siparisId, SiparisDurumu yeniDurum)
        {
            var siparis = await _context.SiparisDb.FindAsync(siparisId);
            if (siparis == null) return false;

            siparis.SiparisDurumu = yeniDurum;
            await _context.SaveChangesAsync();
            return true;
        }
        //----------------------------------------------------------------------------------------------------------------------
        public async Task<bool> SiparisTamamlaAsync(string kullaniciId)
        {
            var hesap = await _context.HBilgileri.FirstOrDefaultAsync(h => h.UserId == kullaniciId);
            if (hesap == null) return false;


            var sepet = await _context.KullaniciSepets
                .Include(s => s.Urunler)
                .FirstOrDefaultAsync(s => s.KullaniciId == kullaniciId);


            if (sepet == null || !sepet.Urunler.Any()) return false;


            var siparisDetaylar = new List<SiparisDetay>();


            foreach (var urunDetay in sepet.Urunler)
            {
                var urun = await _context.Products.FirstOrDefaultAsync(u => u.Id == urunDetay.UrunID);
                if (urun == null || urun.Stock < urunDetay.Adet)
                    return false;

                urun.Stock -= urunDetay.Adet;

                siparisDetaylar.Add(new SiparisDetay
                {
                    UrunId = urun.Id,
                    UrunMiktar = urunDetay.Adet,
                    BirimFiyat = urunDetay.BrutFiyat
                });
            }


            var toplamTutar = siparisDetaylar.Sum(x => x.BirimFiyat * x.UrunMiktar);

            var siparis = new Siparis
            {
                HesapBilgileriId = hesap.HBId,
                AdresId = _context.ABilgileri
                    .Where(a => a.HesapBilgileriId == hesap.HBId && !a.IsDeleted)
                    .Select(a => a.ABId)
                    .FirstOrDefault(),
                SiparisTarihi = DateTime.Now,
                SiparisDurumu = SiparisDurumu.SiparisAlindi,
                S_ToplamTutar = toplamTutar,
                SiparisDetaylar = siparisDetaylar,
                KartIsim = "KART SAHİBİ",
                Odeme = new Odeme
                {
                    KartIsim = "KART SAHİBİ",
                    O_ToplamTutar = toplamTutar
                },
                Fatura = new Fatura
                {
                    MusteriAdi = hesap.Ad,
                    F_ToplamTutar = toplamTutar,
                    SiparisNo = Guid.NewGuid(),
                    FaturaNo = Guid.NewGuid(),
                    SiparisDetaylari = siparisDetaylar
                }
            };


            _context.SiparisDb.Add(siparis);
            _context.KullaniciSepets.Remove(sepet);
            await _context.SaveChangesAsync();
            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------
    }
}
