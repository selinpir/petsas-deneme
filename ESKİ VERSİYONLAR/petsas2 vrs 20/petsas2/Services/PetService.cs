using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class PetService : IPetService
    {
        private readonly ApplicationDbContext _context;
        public PetService(ApplicationDbContext context)
        {
            _context = context;
        }
        //PET EKLEME
        public async Task<PetBilgileri> AddPetAsync(PetBilgileri pet)
        {
            _context.PBilgileri.Add(pet);
            await _context.SaveChangesAsync();
            return pet;
        }

        //PET SILME SOFT DELETE 
        public async Task<bool> DeletePetAsync(int petId)
        {
            var pet = await _context.PBilgileri.FindAsync(petId);
            if (pet == null) return false;

            pet.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        //KAMPANYA VB GIBI KULLANIMLAR ICIN PET TURUNUN ALINMASI-AMA BUNU SUAN KULLANMAYACAGIM/KULLANAMAM GIBI 
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted) // soft delete varsa
                .OrderBy(c => c.PetType)
                .ToListAsync();
        }
        //PETLERIN LISTELENMESI N:M ILISKI BIR KULLANICI BIRDEN FAZLA PET
        public async Task<List<PetBilgileri>> GetPetsByHesapIdAsync(int hesapBilgileriId)
        {
            return await _context.PBilgileri
                .Where(p => p.HesapBilgileriId == hesapBilgileriId && !p.IsDeleted)
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
