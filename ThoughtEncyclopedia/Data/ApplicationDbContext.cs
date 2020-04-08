using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThoughtEncyclopedia.Models;

namespace ThoughtEncyclopedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
    }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Thought> Thoughts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<Thought>().ToTable("Thoughts");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            //App User seed code translaed to DbInitializer
            //ApplicationUser appUser = new ApplicationUser
            //{
            //    UserName = "tester@test.com",
            //    Email = "tester@test.com",
            //    NormalizedEmail = "tester@test.com".ToUpper(),
            //    NormalizedUserName = "tester".ToUpper(),
            //    TwoFactorEnabled = false,
            //    EmailConfirmed = true,
            //    PhoneNumber = "123456789",
            //    PhoneNumberConfirmed = false
            //};

            //PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            //appUser.PasswordHash = ph.HashPassword(appUser, "Password12");
            //modelBuilder.Entity<ApplicationUser>().HasData(
            //    appUser
            //);

            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            //    new IdentityRole { Name = "User", NormalizedName = "USER" }
            //);
        }
    }
}
