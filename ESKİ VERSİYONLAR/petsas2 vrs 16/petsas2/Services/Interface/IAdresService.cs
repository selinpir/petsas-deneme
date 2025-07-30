using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IAdresService
    {
        Task<AdresBilgileri> AddAdresAsync(AdresBilgileri adres);
        Task<List<AdresBilgileri>> GetAdresByHesapIdAsync(int hesapBilgileriId);
        Task<List<Il>> GetIlAsync();
        Task<List<Ilce>> GetIlceAsync();
     
    }
}
