using System.Threading.Tasks;
using petsas2.Migrations;
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IPricingService
    {
        Task<Pricing?> GetCurrentAsync(int productId);
        //fiyat ekle
        Task AddPricing(Pricing pricing);
        //fiyat güncelle
        Task UpdatePricingAsync (Pricing pricing);    
    }
}
