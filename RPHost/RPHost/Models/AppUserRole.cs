using Microsoft.AspNetCore.Identity;

namespace RPHost.Models
{
    public class AppUserRole// : IdentityUserRole<int>
    {
        public User User { get; set; }
        public AppRole Role { get; set; }
    }
}