using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly ApplicationDbContext _context;

        public FaturaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Fatura> GetFaturaByFaturaNo(Guid faturaNo)
        {
            return await _context.FaturaDb
                .Include(f => f.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)                
                .FirstOrDefaultAsync(f => f.FaturaNo == faturaNo);
        }
    }
}

