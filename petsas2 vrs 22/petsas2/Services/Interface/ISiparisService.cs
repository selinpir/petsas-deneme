using petsas2.Models;

namespace petsas2.Services.Interface
{
    public interface ISiparisService
    {
        //SİPARİSİ TAMAMLA +++++++++++++++++++++++++++++++++++++++++++
        Task<bool> SiparisTamamlaAsync(string kullaniciId);

        //SİPARİSLERİ GETİR+++++++++++++++++++++++++++++++++++++++++++
        Task<List<Siparis>> GetKullaniciSiparisleriAsync(int hesapBilgileriId);

        //tek siparis ?
        Task<Siparis> GetSiparisByIdAsync(int siparisId);

        //SİPARİS DURUMU GUNCELLEMESİ-ADMİN +++++++++++++++++++++++++++++++++++++++++++
        Task<bool> SiparisDurumunuGuncelleAsync(int siparisId, SiparisDurumu yeniDurum);
    }
}
