using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IAdresService
    {
        Task<List<AdresBilgileri>> GetAdreslerByUserIdAsync(string userId);
        Task<AdresBilgileri?> GetAdresByIdAsync(int adresId);
        Task AddAdresAsync(AdresBilgileri adres);       
        Task DeleteAdresAsync(int adresId);
    }
}
