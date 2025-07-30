using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;
using System.Security.Claims;

namespace petsas2.Services
{
    public class AdresService : IAdresService
    {
        private readonly ApplicationDbContext _context;

        public AdresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AdresBilgileri>> GetAdreslerByUserIdAsync(string userId)
        {
            return await _context.AdresBilgileris
                .Where(a => a.UserId == userId && !a.IsDeleted)
                .ToListAsync();
        }

        public async Task<AdresBilgileri?> GetAdresByIdAsync(int adresId)
        {
            return await _context.AdresBilgileris.FindAsync(adresId);
        }

        public async Task AddAdresAsync(AdresBilgileri adres)
        {
            _context.AdresBilgileris.Add(adres);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAdresAsync(int adresId)
        {
            var adres = await _context.AdresBilgileris.FindAsync(adresId);
            if (adres != null)
            {
                // Soft delete
                adres.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
