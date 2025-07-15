using petsas2.Models;

namespace petsas2.Services
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