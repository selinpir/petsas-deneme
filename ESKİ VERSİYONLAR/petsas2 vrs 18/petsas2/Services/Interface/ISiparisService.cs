using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ISiparisService
    {
        Task<bool> SiparisTamamlaAsync(string kullaniciId, int adresId, string kartIsim, string odemeYontemi);
    }
}
