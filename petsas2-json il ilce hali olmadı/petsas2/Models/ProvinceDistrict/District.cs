using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsas2.Models
{
    public class District
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }
        public Province Provinces { get; set; }
    }
}

//District : il için 
//[Key] degiskenin birincil anahtar olması icin eklendi
//[ForeignKey(nameof(Province))] -> Hangi ile ait olduğunu Foreign Key ile göster