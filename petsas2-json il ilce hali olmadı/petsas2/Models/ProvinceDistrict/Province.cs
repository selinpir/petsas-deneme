using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace petsas2.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Bir şehrin birden fazla ilçesi olabilir 1:N
        public ICollection<District> Districts { get; set; }
    }
}