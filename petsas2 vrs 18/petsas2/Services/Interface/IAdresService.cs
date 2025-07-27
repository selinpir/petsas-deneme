using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface IAdresService
    {
        //KULLANICI-ADRES ISLEMLERI
        //SUANLIK KULLANICI SADECE ADRES EKLEYEBILIR-GUNCELLEME SILME YOK
        //VE N:N ILISKI KURULDU,BIRDEN FAZLA ADRESI OLAMAZ
        //ADRES EKLEME
        Task<AdresBilgileri> AddAdresAsync(AdresBilgileri adres);
        //ADRES GETIR
        Task<List<AdresBilgileri>> GetAdresByHesapIdAsync(int hesapBilgileriId);
        //TUM IL KAYITLARINI GETIRIR
        Task<List<Il>> GetIlAsync();
        //TUM ILCE KAYITLARINI GETIRIR
        Task<List<Ilce>> GetIlceAsync();     
    }
}
