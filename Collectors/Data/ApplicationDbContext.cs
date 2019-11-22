using System;
using System.Collections.Generic;
using System.Text;
using Collectors.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Collectors.Data
{
    // need to add <ApplicationUser> on IdentityDbContext bc it will automatically use the IdentityUser class
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Collectible> Collectibles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // the 'base' keyword references the parent class (similar to how the 'this' keyword references the current class)
            base.OnModelCreating(modelBuilder);
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            // can pass multiple users into 'HasData' if needed
            modelBuilder.Entity<ApplicationUser>().HasData(user);

        }
    }
}
