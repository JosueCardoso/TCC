using Microsoft.AspNetCore.Identity;

namespace Estimatz.Login.API.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
