using petsas2.Data;

namespace petsas2.Models
{
    public class HesapBilgileri
    {
        //üyeid
        //public int Id { get; set; }

        //// Identity anahtarını ilişkilendir
        //public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        //ad-
        //soyad-
        //tel-
        //dg tarihi-
        //cinsiyet
        //pet adı/
        //hesap oluşturulma tarihi-
        //kredi kartı
        //kredi kart tipi ıd
        //kart son ay
        //kart son yıl
        //fatura ve adres bir sayılacak


    }

    public class AdresBilgileri
    {
        //adres id
        //bir kullanıcının birden fazla adresi olabilir
        //uyeid
        //adres adı
        //il seç
        //ilçe seç
        //açık adres
    }


}

//Siparişler Tablosu
//- order_id (Birincil Anahtar)
//- user_id (Kullanıcı Hesaplarına referans veren Yabancı Anahtar)
//- ödeme yöntemi
//- Shipping_address_id (Yabancı Anahtar referans Adresi)
//- billing_address_id (Yabancı Anahtar referans Adresi)
//- Sipariş durumu
//- sipariş tarihi
//- toplam tutar

//Sipariş Kalemleri Tablosu: Bu tablo, her siparişte
//yer alan ürünleri ve satın alma anındaki miktar ve fiyatını kaydeder.
//- order_item_id (Birincil Anahtar)
//- order_id (Yabancı Anahtar referanslı Siparişler)
//- product_id (ProductCatalog'a referans veren Yabancı Anahtar)
//- miktar
//- fiyat_at_purchase