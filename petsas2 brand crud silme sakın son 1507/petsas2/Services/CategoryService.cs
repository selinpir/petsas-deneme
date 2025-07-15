using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
namespace petsas2.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        //+
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        //+
        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("club not found");
            }
            return category;
        }

        public Task UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
