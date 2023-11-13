using Microsoft.AspNetCore.Identity;

namespace BlazorApp1.Data.Models
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public Role? Role { get; set; }
    }
}
