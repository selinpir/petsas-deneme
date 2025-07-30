using petsas2.Data;
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IUserService
    {
        Task<HesapBilgileri?> GetCurrentUserHesapAsync();
        Task SaveAsync(HesapBilgileri model);
    }
}
