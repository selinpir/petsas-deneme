using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IAdminService
    {
        //ADMIN ICIN KULLANICILARIN LISTELENMESIDIR
        //DAHA SONRA ADMININ KULLANICIYI DEAKTİF ETMESİ VS YAPILABILIR SUAN NO TIME 
        Task<List<HesapBilgileri>> GetAllAsync(); //TUM KAYITLARI LISTE OLARAK GETIRIR   
    }
}
