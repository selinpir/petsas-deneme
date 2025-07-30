using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
            => _context = context;

        // Ürün listesi; marka ve alt kategori bilgileriyle birlikte
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                
                .Include(p => p.SubCategory)
                     .ThenInclude(sc => sc!.Category)
                .Include(p => p.Brand)
                .ToListAsync();
        }
               
        public async Task AddAsync(Product p)
        {
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return;

            existing.IsDeleted = true;        
            await _context.SaveChangesAsync();
        }
        // Belirli bir ürünü getir
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product p)
        {
            var existing = await _context.Products.FindAsync(p.Id);
            if (existing == null)
                throw new Exception("Ürün bulunamadı.");

            // Gerekli alanları güncelle 
            existing.Name = p.Name;
            existing.Description = p.Description;
            //existing.Barcode = p.Barcode;
            existing.ImageUrl = p.ImageUrl;
            existing.Stock = p.Stock;
            existing.MinStock = p.MinStock;
            existing.BrandId = p.BrandId;
            //existing.CategoryId = p.CategoryId;
            existing.SubCategoryId = p.SubCategoryId;

            await _context.SaveChangesAsync();
        }
    }
}
