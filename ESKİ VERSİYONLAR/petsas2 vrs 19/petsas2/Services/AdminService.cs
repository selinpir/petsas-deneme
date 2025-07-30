using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<HesapBilgileri>> GetAllAsync()
        {
            //KULLANICININ IL-ILCE-PETTURU BILGISI CEKILDI
            return await _context.HBilgileri
              .Where(h => !h.IsDeleted)
                  .Include(h => h.AdresBilgisi)
                      .ThenInclude(a => a.Il)

                  .Include(h => h.AdresBilgisi)
                      .ThenInclude(a => a.Ilce)

                  .Include(h => h.PBilgileri)
                      .ThenInclude(p => p.Category)

              .ToListAsync();
        }
        //TÜM SİPARİŞLERİ LİSTELE
        public async Task<List<Siparis>> GetTumSiparislerAsync()
        {
            return await _context.SiparisDb
                .Include(s => s.HesapBilgileri)
                .Include(s => s.Adres)
                .Include(s => s.SiparisDetaylar)
                    .ThenInclude(d => d.Urun)
                .Include(s => s.Odeme)
                .Include(s => s.Fatura)
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();
        }
        //TEK SİPARİŞLERİ LİSTELE
        //public async Task<List<Siparis>> GetSiparislerByKullaniciIdAsync(string kullaniciId)
        //{
        //    int hesapId = int.Parse(kullaniciId);
        //    return await _context.SiparisDb
        //        .Include(s => s.SiparisDetaylar)
        //        .Include(s => s.HesapBilgileri)
        //        .Where(s => s.HesapBilgileriId == hesapId)
        //        .ToListAsync();
        //}

    }
}
