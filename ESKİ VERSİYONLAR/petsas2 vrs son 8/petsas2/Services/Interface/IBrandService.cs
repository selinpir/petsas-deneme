//marka yonetimi icin interface
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<Brand?> GetByIdAsync(int id);

        //ekleme
        Task AddAsync(Brand brand);

        //silme
        Task SoftDeleteAsync(int id);

        //güncelleme
        Task UpdateAsync(Brand brand);

    }
}