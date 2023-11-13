using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Data.Models
{
    public class Role : IdentityRole
    {
        [Required]
        public override string Name { get => base.Name; set => base.Name = value; }

        public List<RoleClaim>? RoleClaims { get; set; }

        public List<UserRole>? UserRoles { get; set; }
    }
}
