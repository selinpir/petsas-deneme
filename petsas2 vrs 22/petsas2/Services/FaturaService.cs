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


        //public async Task<Fatura> GetFaturaByFaturaNo(int FaturaId)
        //{
        //    return await _context.FaturaDb
        //.Include(f => f.SiparisDetaylari)
        //    .ThenInclude(sd => sd.Urun)
        //.SingleOrDefaultAsync(f => f.FaturaId == FaturaId);
        //}


        //-------------------------------------------------------------------
        public async Task<Fatura> GetFaturaByFaturaNo(int FaturaId)
        {
            //    return await _context.FaturaDb

            //        .Include(f => f.Siparis)
            //            .ThenInclude(s => s.SiparisDetaylar)
            //                .ThenInclude(sd => sd.Urun)
            //        //.Include(f => f.HesapBilgileri)
            //        .FirstOrDefaultAsync(f => f.FaturaId == FaturaId);

            return await _context.FaturaDb
            .Include(f => f.Siparis)
                .ThenInclude(s => s.SiparisDetaylar)
                  .ThenInclude(sd => sd.Urun!)
            .FirstOrDefaultAsync(f => f.FaturaId == FaturaId);

        }
    }
}

//SingleOrDefaultAsync:Koşula uyan bir tane ya da hiç kayıt olmasını bekler. Birden fazla kayıt varsa hata fırlatır.

//FirstAsync: Koşula uyan ilk kaydı getirir, eğer hiç yoksa exception (hata) fırlatır.

//SingleAsync(f => f.FaturaNo == faturaNo): Koşula uyan tek kaydı getirir, kayıt yoksa veya birden fazla varsa hata fırlatır.

//Where(f => f.FaturaNo == faturaNo).ToListAsync(): Koşula uyan tüm kayıtları liste olarak getirir (birden fazla olabilir).