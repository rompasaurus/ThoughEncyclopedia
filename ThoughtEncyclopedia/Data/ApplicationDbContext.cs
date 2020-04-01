using System;
using System.Collections.Generic;
using System.Text;
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
            //modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<Topic>().ToTable("Topic");
            modelBuilder.Entity<Thought>().ToTable("Thoughts");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Comment>().ToTable("Comments");
        }
    }
}
