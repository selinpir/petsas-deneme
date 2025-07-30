using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;
        public SubCategoryService(ApplicationDbContext context)
            => _context = context;

        // Tüm alt kategorileri, sadece silinmemiş olanları, ilişkili Category ile beraber döner
        public async Task<List<SubCategory>> GetAllAsync()
        {
            return await _context.SubCategories
                .Include(sc => sc.Category)
                .Where(sc => !sc.IsDeleted)
                .ToListAsync();
        }
        // Belirli bir alt kategoriyi, silinmemişse ve ilişkili Category ile beraber döner
        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            return await _context.SubCategories
                .Include(sc => sc.Category)
                .FirstOrDefaultAsync(sc => sc.Id == id && !sc.IsDeleted);
        }
        //Yeni alt kategori ekleme
        public async Task AddAsync(SubCategory sub)
        {
            _context.SubCategories.Add(sub);
            await _context.SaveChangesAsync();
        }
        //Var olan alt kategoriyi güncelelme
        public async Task Update(SubCategory sub)
        {
            var existing = await _context.SubCategories.FindAsync(sub.Id);
            if (existing == null || existing.IsDeleted)
                throw new Exception("Alt kategori bulunamadı veya silinmiş.");
            existing.ProductName = sub.ProductName;
            existing.CategoryId = sub.CategoryId;
            await _context.SaveChangesAsync();
        }
        //Softdelete
        public async Task SoftDelete(int id)
        {
            var existing = await _context.SubCategories.FindAsync(id);
            if (existing == null)
                return;

            existing.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
