//sayfa için gerekli yorumlar yazılmadı henüz---

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

        //ürün ekle düz
        public async Task AddAsync(Product p)
        {
            _context.Products.Add(p);
            await _context.SaveChangesAsync();
        }

        //sipariş vs gibi kısımlarda yani eski kayıtlarda sorun olmaması için sadece silinMİŞ gibi yaptım
        //tamamen silinmemekte ama silinMİŞ olanlar hiçbir işlemimizde olmayacaktır
        public async Task SoftDeleteAsync(int id)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return;

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        //ID ye göre belirli bir ürünü getir
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //ürün güncelleme HENÜZ YAPILMADI
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

        //stok azalırsa uyarı vermesi içindir (stock<=minstock)
        public async Task<List<StockAlertDto>> GetStockAlertAsync()
        {
            return await _context.Products
             .Where(p => p.Stock <= p.MinStock)
              .Select(p => new StockAlertDto
              {
                  ProductId = p.Id,
                  ProductName = p.Name,
                  Stock = p.Stock,
                  MinStock = p.MinStock
              })
               .ToListAsync();
        }

        //stoğu azalan ürünler-> stok ekle
        //sadece stok azalan ürünler listelenir ve bunlara ekleme yapılır
        public async Task AddStockAsync(int productId, int amount)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            product.Stock += amount;
            await _context.SaveChangesAsync();
        }

        //ürün listesi->stok ekle
        //normal stok ekleme
        public async Task AddNormStockAsync(int productId, int stock)
        {
            var p = await _context.Products.FindAsync(productId);
            if (p == null) throw new InvalidOperationException("Ürün bulunamadı.");
            p.Stock += stock;
            _context.Products.Update(p);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SPrdctListDto>> GetAllForSupplierAsync()
        {
            return await _context.Products
       .Select(p => new SPrdctListDto
       {
           ProductId = p.Id,
           Barcode = (Guid)p.Barcode,
           ProductName = p.Name,
           Brand = p.Brand.Name,
           ImageUrl = p.ImageUrl,
           Stock = p.Stock,
           MinStock = p.MinStock
       })
       .ToListAsync();
        }

        public async Task<List<Product>> GetProductsAsync(string? category, string? subCategory)
        {
            var query = _context.Products
        .Include(p => p.Pricings)
        .Include(p => p.Category)
        .AsQueryable();

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.PetType == category);

            if (!string.IsNullOrEmpty(subCategory))
                query = query.Where(p => p.SubCategory.ProductName == subCategory);

            return await query.ToListAsync();
        }
    }
}
