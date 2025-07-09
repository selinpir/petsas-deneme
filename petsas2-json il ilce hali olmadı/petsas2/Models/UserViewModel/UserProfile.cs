using petsas2.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using static MudBlazor.CategoryTypes;
using static MudBlazor.Colors;
using petsas2.Models;

namespace petsas2.Models.UserViewModel
{
    public class UserProfile
    {
        //kullanıcı kaydının devamını gerceklestirmek zorundadır ve kullanıcının geri kalan bilgileri alınmaktadır
        //profil bilgileri
        [Key, ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Ad zorunlu")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunlu")]
        public string LastName { get; set; }

        public enum GenderType { Erkek, Kadın, Diğer }
        public GenderType Gender { get; set; }

        //yaş kısıtlaması 18 küçük giremez
        [Required(ErrorMessage = "Doğum Tarihi zorunlu")]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunlu")]
        public string PhoneNumber { get; set; }

        //siparis isleminin gerceklestirilebilmesi icin
        public bool ProfileCompleted { get; set; }

        //Adres bilgileri
        [Required(ErrorMessage = "Adres adı zorunlu")]
        public string AddressName { get; set; }

        [Required]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Required(ErrorMessage = "Açık adres zorunlu")]
        public string DetailedAddress { get; set; }

        //sadece sipariş kısmında alınacağı için adres bilgilerine eklendi değişebilir ?     
        [Required, StringLength(11, MinimumLength = 11, ErrorMessage = "TC kimlik 11 haneli olmalıdır.")]
        public string TcIdentity { get; set; }

    }
}




