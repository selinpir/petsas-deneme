using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IProductService
    {
        //GENEL
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product p);       
        Task SoftDeleteAsync(int id);
        //GENEL

        //dto: data transfer object : veri aktarım nesnesi
        Task<List<StockAlertDto>> GetStockAlertAsync();

        //STOK
        //kritik stok verenlere stok ekle
        Task AddStockAsync(int productId, int amount);
        //normal bir ürüne stok ekle
        Task AddNormStockAsync(int productId, int stock);
        //STOK

        //ürünleri tedarikçi için listele
        Task<List<SPrdctListDto>> GetAllForSupplierAsync();

        //ürünleri kategorileri bağlı alt kategorilerine çekmek ve gerekli urllere yonlendirmek için
        Task<List<Product>> GetProductsAsync(string? category, string? subCategory);

        //ürün detay sayfasının altında baKabileceğiniz diğer ürünler yapısının oluşturulması için 2207        
        Task<List<Product>> GetRandomProductsAsync(int currentProductId);
    }
}
