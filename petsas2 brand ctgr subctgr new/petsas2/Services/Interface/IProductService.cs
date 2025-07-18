//sayfa için gerekli yorumlar yazılmadı henüz---
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

        //dto: data transfer object : veri aktarım nesnesi
        Task<List<StockAlertDto>> GetStockAlertAsync();    
        
        //kritik stok verenlere stok ekle
        Task AddStockAsync(int productId, int amount);

        //normal bir ürüne stok ekle
        Task AddNormStockAsync(int productId, int stock);
    }  
}
