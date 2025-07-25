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
            //kullanici bilgilerini çekme il ilce pet türü
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

        public async Task<HesapBilgileri?> GetByIdAsync(int HBId)
        {
            return await _context.HBilgileri
                .Include(h => h.User)
                .FirstOrDefaultAsync(h => h.HBId == HBId && !h.IsDeleted);
        }

    }
}
