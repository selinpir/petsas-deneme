using System.Threading.Tasks;
using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IPricingService
    {
        Task<Pricing?> GetCurrentAsync(int productId);
        Task UpsertPricingAsync(Pricing pricing);
    
    }
}
