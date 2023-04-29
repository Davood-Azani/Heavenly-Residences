using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
