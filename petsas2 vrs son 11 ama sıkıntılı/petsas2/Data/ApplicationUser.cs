using Microsoft.AspNetCore.Identity;
using petsas2.Models;

namespace petsas2.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AdresBilgileri> Adresler { get; set; } = new List<AdresBilgileri>();
    }

}
