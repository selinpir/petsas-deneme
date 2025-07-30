using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IHesapService
    {
        Task<HesapBilgileri?> GetCurrentUserHesapAsync();   
        Task SaveAsync(HesapBilgileri model);     //HESAP EKLE
    }
}
