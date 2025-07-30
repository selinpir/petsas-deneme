using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class AdresService : IAdresService
    {
        private readonly ApplicationDbContext _context;

        public AdresService(ApplicationDbContext context)
        {
            _context = context;
        }
        //Adres Ekleme
        public async Task<AdresBilgileri> AddAdresAsync(AdresBilgileri adres)
        {
            _context.ABilgileri.Add(adres);
            await _context.SaveChangesAsync();
            return adres;
        }

        public async Task<List<AdresBilgileri>> GetAdresByHesapIdAsync(int hesapBilgileriId)
        {
            return await _context.ABilgileri
                .Where(a => a.HesapBilgileriId == hesapBilgileriId && !a.IsDeleted)
                .Include(a => a.Il)
                .Include(a => a.Ilce)
                .ToListAsync();
        }


        public async Task<List<Il>> GetIlAsync()
        {
            return await _context.IlDb
                 .OrderBy(il => il.Id)
                 .ToListAsync();
        }

        public async Task<List<Ilce>> GetIlceAsync()
        {
            return await _context.IlceDb
                   .OrderBy(ilce => ilce.Id)
                   .ToListAsync();
        }
    }
}
