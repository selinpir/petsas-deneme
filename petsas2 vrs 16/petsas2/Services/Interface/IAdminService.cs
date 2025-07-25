using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IAdminService
    {
        //kullaniciları listele
        Task<List<HesapBilgileri>> GetAllAsync();
        Task<HesapBilgileri?> GetByIdAsync(int HBId);     
    }
}
