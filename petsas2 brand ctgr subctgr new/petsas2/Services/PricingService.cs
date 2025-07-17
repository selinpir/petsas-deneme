using Humanizer;
using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class PricingService : IPricingService
    { 
        private readonly ApplicationDbContext _db;
        public PricingService(ApplicationDbContext db)
        => _db = db;       

        //mevcut fiyat
        public async Task<Pricing?> GetCurrentAsync(int productId)
        {
            return await _db.Pricings
                .Where(p => p.ProductId == productId && p.FiyatSonlanmaTarihi == null)
                .OrderByDescending(p => p.FiyatBaslangicTarihi)
                .FirstOrDefaultAsync();
        }

        //fiyat ekle/güncelle
        public async Task UpsertPricingAsync(Pricing pricing)
        {
            var now = DateTime.UtcNow;

            // varolan aktif kaydı kapat
            var eski = await _db.Pricings
                .FirstOrDefaultAsync(p => p.ProductId == pricing.ProductId
                                      && p.FiyatSonlanmaTarihi == null);

            if (eski != null)
            {
                eski.FiyatSonlanmaTarihi = now;
                _db.Pricings.Update(eski);
            }

            // yeni kaydı ekle 
            var yeni = new Pricing
            {
                ProductId = pricing.ProductId,
                NetFiyat = pricing.NetFiyat,
                IndirimOrani = pricing.IndirimOrani,
                KdvOrani = pricing.KdvOrani,
                FiyatBaslangicTarihi = now,
                FiyatSonlanmaTarihi = null
            };
            _db.Pricings.Add(yeni);

            await _db.SaveChangesAsync();
        }
    }
}
