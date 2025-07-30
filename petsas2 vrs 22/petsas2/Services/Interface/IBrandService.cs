using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync(); //TUM MARKALARI GETIR
        Task<Brand?> GetByIdAsync(int id);
        Task AddAsync(Brand brand); //MARKA EKLE
        Task SoftDeleteAsync(int id);  //MARKA SIL
        Task UpdateAsync(Brand brand);  //MARKA GUNCELLE
    }
}