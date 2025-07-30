using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IFaturaService
    {
        //bu admin için çalışıypr ama hatalı 
        Task<Fatura> GetFaturaByNoAsync(string faturaNo);
        //bu denenecek
        //Task<Fatura?> GetFaturaByNoAsync(Guid faturaNo);       
        //bu denenecek çalışmıyper
        Task<Fatura?> GetFaturaBySiparisNoAsync(Guid siparisNo);
    
    }
}
