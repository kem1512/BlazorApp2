using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data.Models
{
    public class User : IdentityUser
    {
        public List<UserRole>? UserRoles { get; set; }
    }
}
