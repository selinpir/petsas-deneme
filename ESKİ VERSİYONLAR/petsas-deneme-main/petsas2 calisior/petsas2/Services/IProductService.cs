using petsas2.Models;

namespace petsas2.Services
{
    //Interface’de metodların amacı ne yapılacağını söylemektir
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        //ekle
        Task AddAsync(Product product); 
        //güncelle
        Task UpdateAsync(Product product);
        //sil
        Task DeleteAsync(int id);   
    }
}
