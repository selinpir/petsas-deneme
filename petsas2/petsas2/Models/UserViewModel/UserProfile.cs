using petsas2.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

//PROFILE COMPLETE İSLEVİNİ UNUTMA
//YAS VE TELEFON NO KONTROLU YAPILACAK

namespace petsas2.Models.UserViewModel
{
    public class UserProfile
    {
        //kullanıcı kaydının devamını gerceklestirmek zorundadır ve kullanıcının geri kalan bilgileri alınmaktadır
        //sipariş işlemleri profil bilgileri ve adres bilgileri eksikse devam edilmemelidir HENÜZ YAPILMADI
        //tedarikçi ve admin de oldugu icin tabloyu orada genisletmek cok fazla null alan olmasına sebep olacaktı
        
        //profil bilgileri
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Ad zorunlu")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunlu")]
        public string LastName { get; set; }

        public GenderType Gender { get; set; }

        //yaş kısıtlaması 18 küçük giremez HENUZ YAPILMADI ya da yapılmalı mı
        [Required(ErrorMessage = "Doğum Tarihi zorunlu")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunlu")]
        public string PhoneNumber { get; set; }

        //siparis isleminin gerceklestirilebilmesi icin-daha sonra kullanılacak
        public bool ProfileCompleted { get; set; }

        //sadece sipariş kısmında alınacağı için adres bilgilerine eklendi değişebilir ?     
        [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "TC kimlik 11 haneli olmalıdır.")]
        public string TcIdentity { get; set; }
    }
}
