using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IPetService
    {
        Task<PetBilgileri> AddPetAsync(PetBilgileri pet);
        Task<bool> DeletePetAsync(int petId);
        Task<List<PetBilgileri>> GetPetsByHesapIdAsync(int hesapBilgileriId);
        Task<List<Category>> GetCategoriesAsync();
    }
}
