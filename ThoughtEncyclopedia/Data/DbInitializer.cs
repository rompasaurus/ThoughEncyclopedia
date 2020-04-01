using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtEncyclopedia.Models;

namespace ThoughtEncyclopedia.Data
{
    public class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            //Check if data values already exist in the DB
            if (context.Users.Any())
            {
                return;
            }
            string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            //Roles not yet initialized in startup
            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }
            Console.WriteLine("Seeeeeding");
            var User1 = new ApplicationUser
            {
                FirstName = "Michael",
                LastName = "McHenry",
                Email = "xxxx@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };



            if (!context.Users.Any(u => u.UserName == User1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(User1, "secret");
                User1.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(User1);

            };
            context.Users.Add(User1);
            context.SaveChanges();

            var Categories = new Category[]
            {
                new Category{CategoryId=0,CategoryName="Personal Thoughts",Description="Thoughts That are personal to me"},
                new Category{CategoryId=1,CategoryName="Project Thoughts",Description="Thoughts related to personal projects"}
            };
            foreach (Category c in Categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var Topics = new Topic[]
            {
                new Topic{TopicID=0,ParentID=0,Title="3 Gratitudes",Description="I'm grateful for \n I'm grateful for \n I'm grateful for",User=User1,Category=Categories[0]}
            };
            foreach (Topic t in Topics)
            {
                context.Topics.Add(t);
            }
            context.SaveChanges();
        }

    }

}
