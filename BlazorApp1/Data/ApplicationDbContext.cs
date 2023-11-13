using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data.Models;
using Microsoft.CodeAnalysis;
using NuGet.Packaging.Signing;
using System.Reflection.Emit;

namespace BlazorApp1.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin,
        RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(a =>
            {
                a.HasOne(b => b.Role).WithMany(c => c.UserRoles).HasForeignKey(d => d.RoleId);
                a.HasOne(b => b.User).WithMany(c => c.UserRoles).HasForeignKey(d => d.UserId);
            });

            builder.Entity<RoleClaim>(a =>
            {
                a.HasOne(b => b.Role).WithMany(c => c.RoleClaims).HasForeignKey(d => d.RoleId);
            });

        }


        public DbSet<Role>? Role { get; set; }

        public DbSet<RoleClaim>? RoleClaim { get; set; }

        public DbSet<User>? User { get; set; }

        public DbSet<UserClaim>? UserClaim { get; set; }

        public DbSet<UserLogin>? UserLogin { get; set; }

        public DbSet<UserRole>? UserRole { get; set; }

        public DbSet<UserToken>? UserToken { get; set; }


    }
}