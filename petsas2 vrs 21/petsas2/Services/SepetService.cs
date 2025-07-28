using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using petsas2.Data;
using petsas2.Models;
using petsas2.Services.Interface;

namespace petsas2.Services
{
    public class SepetService : ISepetService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authStateProvider;
        public SepetService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, AuthenticationStateProvider authStateProvider)
        {
            _context = context;
            _userManager = userManager;
            _authStateProvider = authStateProvider;
        }
        //kullaniciId int değil eşleştirme için string olmalıymış düzeltildi
        //SEPETE EKLE----------------------------------------------------------------------------------------------
        public async Task<bool> SepeteEkleAsync(int productId, int adet = 1)
        {
            try
            {
                var authstate = await _authStateProvider.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (!user.Identity.IsAuthenticated) return false;

                var appUser = await _userManager.GetUserAsync(user);
                if (appUser == null) return false;

                //  ROL KONTROLÜ
                if (!await _userManager.IsInRoleAsync(appUser, "User"))
                    return false;

                // Ürün + fiyat
                var urun = await _context.Products
                    .Include(p => p.Pricings)
                    .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted);
                if (urun == null) return false;

                var fiyat = urun.Pricings
                    .OrderByDescending(p => p.FiyatId)
                    .FirstOrDefault()?.BrutFiyat ?? 0;

                // Sepet al veya oluştur
                var kullaniciSepet = await _context.KullaniciSepets
                    .Include(ks => ks.Urunler)
                    .FirstOrDefaultAsync(ks => ks.KullaniciId == appUser.Id);

                if (kullaniciSepet == null)
                {
                    kullaniciSepet = new KullaniciSepet
                    {
                        KullaniciId = appUser.Id,
                        Urunler = new List<SepetDetay>()
                    };
                    _context.KullaniciSepets.Add(kullaniciSepet);
                }

                var mevcutUrun = kullaniciSepet.Urunler.FirstOrDefault(u => u.UrunID == productId);
                if (mevcutUrun != null)
                {
                    mevcutUrun.Adet += adet;
                }
                else
                {
                    var sepetDetay = new SepetDetay
                    {
                        UrunID = productId,
                        UrunAd = urun.Name,
                        Gorsel = urun.ImageUrl,
                        BrutFiyat = fiyat,
                        Adet = adet,
                        KullaniciSepetId = kullaniciSepet.Id
                    };
                    kullaniciSepet.Urunler.Add(sepetDetay);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //SEPETE EKLE----------------------------------------------------------------------------------------------
        //SEPET DETAY----------------------------------------------------------------------------------------------
        public async Task<List<SepetDetay>> GetSepetUrunleriAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated) return new List<SepetDetay>();

            var appUser = await _userManager.GetUserAsync(user);
            if (appUser == null) return new List<SepetDetay>();

            var KullaniciSepet = await _context.KullaniciSepets
                .Include(ks => ks.Urunler)
                .FirstOrDefaultAsync(ks => ks.KullaniciId == appUser.Id);

            return KullaniciSepet?.Urunler ?? new List<SepetDetay>();
        }
        //SEPET DETAY----------------------------------------------------------------------------------------------
        public async Task<KullaniciSepet> SepetUrunleriAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (!user.Identity.IsAuthenticated)
                return null;

            var appUser = await _userManager.GetUserAsync(user);
            if (appUser == null)
                return null;

            var sepet = await _context.KullaniciSepets
                .Include(s => s.Urunler)
                .FirstOrDefaultAsync(s => s.KullaniciId == appUser.Id);

            return sepet;
        }
        //URUN ADET AZALT ARTTIR-----------------------------------------------------------------------------------
        public async Task<bool> UrunAdetGuncelleAsync(int productId, int yeniAdet)
        {
            try
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (!user.Identity.IsAuthenticated) return false;

                var appUser = await _userManager.GetUserAsync(user);
                if (appUser == null) return false;

                var kullaniciSepet = await _context.KullaniciSepets
                    .Include(s => s.Urunler)
                    .FirstOrDefaultAsync(s => s.KullaniciId == appUser.Id);

                if (kullaniciSepet == null) return false;

                var urun = kullaniciSepet.Urunler.FirstOrDefault(x => x.UrunID == productId);
                if (urun == null) return false;

                urun.Adet = yeniAdet;
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        //URUN ADET AZALT ARTTIR-----------------------------------------------------------------------------------
        //URUN SİL-------------------------------------------------------------------------------------------------
        public async Task<bool> UrunCikarAsync(int productId)
        {
            try
            {
                var authState = await _authStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                if (!user.Identity.IsAuthenticated) return false;

                var appUser = await _userManager.GetUserAsync(user);
                if (appUser == null) return false;

                var kullaniciSepet = await _context.KullaniciSepets
                    .Include(s => s.Urunler)
                    .FirstOrDefaultAsync(s => s.KullaniciId == appUser.Id);

                if (kullaniciSepet == null) return false;

                var urun = kullaniciSepet.Urunler.FirstOrDefault(x => x.UrunID == productId);
                if (urun == null) return false;

                kullaniciSepet.Urunler.Remove(urun); // ilişkili listeden çıkarıyoruz
                _context.Sepets.Remove(urun);   // veritabanından da siliyoruz

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }      
    }
}
