using Microsoft.AspNetCore.Identity;

namespace Estimatz.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
