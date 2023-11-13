using Microsoft.AspNetCore.Identity;

namespace BlazorApp1.Data.Models
{
    public class UserRole: IdentityUserRole<string>
    {
        public User? User { get; set; }

        public Role? Role { get; set; }
    }
}
