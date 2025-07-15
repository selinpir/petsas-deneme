using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _context;

        public BrandService(ApplicationDbContext context)
        {
            _context = context;
        }

        //silinenleri göstermemesi için
        public async Task<List<Brand>> GetAllAsync()
        {
            return await _context.Brands
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }
        //ekle
        public async Task AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }
        //silme işlemleri için
        public async Task SoftDeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand != null)
            {
                brand.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        //guncelle
        public async Task UpdateAsync(Brand brand)
        {
            var existing = await _context.Brands.FindAsync(brand.Id);
            if (existing == null || existing.IsDeleted)
                throw new Exception("Marka bulunamadı veya silinmiş.");

            existing.Name = brand.Name;
            await _context.SaveChangesAsync();
        }

    }
}
