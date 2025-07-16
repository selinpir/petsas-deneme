using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product p);
        Task UpdateAsync(Product p);
        Task SoftDeleteAsync(int id);
    }  
}
