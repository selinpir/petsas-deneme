//INTERFACE-KATEGORI YONETIMI
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?>GetByIdAsync(int id);
        //kategori ekleme
        Task AddAsync(Category category);
        //kategori silme
        Task SoftDeleteAsync(int id);
        //kategori güncelleme
        Task UpdateAsync(Category category);
    }
}
