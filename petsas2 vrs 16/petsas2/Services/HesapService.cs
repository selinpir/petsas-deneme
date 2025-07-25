using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class HesapService : IHesapService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authStateProvider;
        public HesapService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, AuthenticationStateProvider authStateProvider)
        {
            _context = context;
            _userManager = userManager;
            _authStateProvider = authStateProvider;
        }
        //HESABI GETİR
        public async Task<HesapBilgileri?> GetCurrentUserHesapAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not { IsAuthenticated: true }) return null;

            var appUser = await _userManager.GetUserAsync(user);
            if (appUser == null) return null;

            return await _context.HBilgileri
                .FirstOrDefaultAsync(h => h.UserId == appUser.Id && !h.IsDeleted);

        }
        //HESAP EKLE
        public async Task SaveAsync(HesapBilgileri model)
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity is not { IsAuthenticated: true }) throw new Exception("Giriş yapan kullanıcı bulunamadı.");

            var appUser = await _userManager.GetUserAsync(user);
            if (appUser == null) throw new Exception("Kullanıcı verisi alınamadı.");
            var mevcut = await _context.HBilgileri.FirstOrDefaultAsync(h => h.UserId == appUser.Id && !h.IsDeleted);

            if (mevcut == null)
            {
                model.UserId = appUser.Id;
                model.HesapOlusturmaTarihi = DateTime.UtcNow;
                model.IsCompleted = true;
                _context.HBilgileri.Add(model);
            }
            else
            {
                mevcut.Ad = model.Ad;
                mevcut.Soyad = model.Soyad;
                mevcut.Telefon = model.Telefon;
                mevcut.DogumTarihi = model.DogumTarihi;
                mevcut.CinsiyetTipi = model.CinsiyetTipi;
                mevcut.IsCompleted = true;
            }

            await _context.SaveChangesAsync();

        }
        //HESAP SİL

    }
}
