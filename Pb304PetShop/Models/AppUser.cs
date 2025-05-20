using Microsoft.AspNetCore.Identity;

namespace Pb304PetShop.Models
{
    public class AppUser: IdentityUser
    {
        public required string FullName { get; set; }
      
    }
   
}
