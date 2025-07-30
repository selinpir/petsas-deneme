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
        //fatura no ya gore getir faturayı
        public async Task<Fatura> GetFaturaByNoAsync(string faturaNo)
        {
            if (!Guid.TryParse(faturaNo, out var faturaGuid))
                return null;

            return await _context.FaturaDb
                .Include(f => f.SiparisDetaylari)
                .FirstOrDefaultAsync(f => f.FaturaNo == faturaGuid);
        }

        //public async Task<Fatura?> GetFaturaByNoAsync(Guid faturaNo)
        //{
        //    return await _context.FaturaDb
        //     .Include(f => f.Siparis)
        //         .ThenInclude(s => s.SiparisDetaylar)
        //             .ThenInclude(sd => sd.Urun)
        //     .FirstOrDefaultAsync(f => f.FaturaNo == faturaNo);
        //}

        //siparis no ya gore fatura getir


        public async Task<Fatura?> GetFaturaBySiparisNoAsync(Guid siparisNo)
        {
            return await _context.FaturaDb
              .Include(f => f.Siparis)
                  .ThenInclude(s => s.SiparisDetaylar)
                      .ThenInclude(sd => sd.Urun)
              .FirstOrDefaultAsync(f => f.SiparisNo == siparisNo);
        }


    }
}
