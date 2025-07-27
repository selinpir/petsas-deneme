using petsas2.Models;
namespace petsas2.Services.Interface
{
    public interface ISepetService
    {
        //SEPETE EKLEME İSLEMİ
        Task<bool> SepeteEkleAsync(int productId, int adet = 1);
        //SEPET DETAY
        Task<List<SepetDetay>> GetSepetUrunleriAsync();
        //SEPETTEN ÜRÜNÜ SİL
        Task<bool> UrunCikarAsync(int productId);
        //ÜRÜNÜN SAYISINI ARTTIR 
        Task<bool> UrunAdetGuncelleAsync(int productId, int yeniAdet);

        Task<KullaniciSepet> SepetUrunleriAsync();
    }
}
