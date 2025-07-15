using petsas2.Models;

namespace petsas2.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int id);
    }
}
