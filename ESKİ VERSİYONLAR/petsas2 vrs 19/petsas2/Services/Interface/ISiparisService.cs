using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ISiparisService
    {
        //SİPARİSİN TAMAMLANMASI --
        Task<bool> SiparisTamamlaAsync(string kullaniciId, int adresId, string kartIsim, string odemeYontemi);


        //KULLANICI-KENDİ SİPARİSİ--
        Task<List<Siparis>> GetKullaniciSiparisleriAsync(int hesapBilgileriId);


        //SİPARİS DURUMUNNU GUNCELLE--
        Task<bool> SiparisDurumunuGuncelleAsync(int siparisId, SiparisDurumu yeniDurum);


    }
}
