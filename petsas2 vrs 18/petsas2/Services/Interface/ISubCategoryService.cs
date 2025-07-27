using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetAllAsync();
        Task<SubCategory?> GetByIdAsync(int id);
        Task AddAsync (SubCategory sub);
        Task Update (SubCategory sub);
        Task SoftDelete(int id);
    }
}

