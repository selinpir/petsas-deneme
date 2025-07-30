//INTERFACE-MARKA YONETİMİ
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllAsync();

        Task<Brand?> GetByIdAsync(int id);
        //MARKA EKLE   
        Task AddAsync(Brand brand);
        //MARKA SIL-SOFTDELETE
        Task SoftDeleteAsync(int id);
        //MARKA GUNCELLE
        Task UpdateAsync(Brand brand);

    }
}