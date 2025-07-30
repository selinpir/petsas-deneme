using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?>GetByIdAsync(int id);     
        Task AddAsync(Category category); 
        Task SoftDeleteAsync(int id);    
        Task UpdateAsync(Category category);
    }
}
