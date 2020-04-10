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
            Console.WriteLine("Seeding Database");
            var User1 = new ApplicationUser
            {
                FirstName = "Michael",
                LastName = "McHenry",
                Email = "test@example.com",
                NormalizedEmail = "test@example.com".ToUpper(),
                UserName = "test@example.com",
                NormalizedUserName = "test@example.com".ToUpper(),
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };



            if (!context.Users.Any(u => u.UserName == User1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(User1, "Password123");
                User1.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(User1);

            };
            context.Users.Add(User1);
            

            var Categories = new Category[]
            {
                new Category{CategoryName="Personal Thoughts",  Description="Thoughts That are personal to me"},
                new Category{CategoryName="Project Thoughts",   Description="Thoughts related to personal projects"},
                new Category{CategoryName="Literary Thoughts",  Description="Thoughts about the latest literature"},
                new Category{CategoryName="News Thoughts",      Description="Thoughts about whats happening in the world today"},
            };
            foreach (Category c in Categories)
            {
                context.Categories.Add(c);
            }
            

            var Topics = new Topic[]
            {
                new Topic{Title="3 Gratitudes",     Description="I'm grateful for \n I'm grateful for \n I'm grateful ",    User=User1,Category=Categories[0]},
                new Topic{Title="Song of the Day",  Description="Artist Name: \n Album Name: \n Song Title: \n",            User=User1,Category=Categories[0]},
                new Topic{Title="Today's Tasks",    Description="Listing of scheduled events,todo and happenings",          User=User1,Category=Categories[1]},
                new Topic{Title="Food",    Description="List of food eaten today along with calorie total estimates",          User=User1,Category=Categories[0]},
                new Topic{Title="Exercise",    Description="What I did for exercise today",          User=User1,Category=Categories[0]},
            };
            foreach (Topic t in Topics)
            {
                context.Topics.Add(t);
            }

            var Thoughts = new Thought[]
            {
                new Thought{Topic=Topics[0],DateCreated=new DateTime(2020,4,8),ContentText="I'm grateful for new bike days \n I'm grateful for the start of bowling leagues \n I'm grateful sweeping wins",  User = User1},
                new Thought{Topic=Topics[1],DateCreated=new DateTime(2020,4,8),ContentText="Artist Name: GHOST DATA \n Album Name: Cruel Choreography \n Song Title: Queen's Game \n",      User = User1},
                new Thought{Topic=Topics[2],DateCreated=new DateTime(2020,4,8),ContentText="Finish Database Seed Data \n Present Seed Data as html on site \n Eat Lunch \n",                User = User1},
                new Thought{Topic=Topics[3],DateCreated=new DateTime(2020,4,8),ContentText="Mighty Wings				500\nBreaded chicked breast		400\n2 ribs 						200\nfried okra 					300\npeas 						100\nApple						 80\n\tTotal					1600 Calories",  User = User1},
                new Thought{Topic=Topics[4],DateCreated=new DateTime(2020,4,8),ContentText="Ridgeline Trail > 10000 steps",      User = User1},
                new Thought{Topic=Topics[0],DateCreated=new DateTime(2020,4,9),ContentText="I'm grateful for wet shits \n I'm grateful for indigestion \n I'm grateful working Databases",  User = User1},
                new Thought{Topic=Topics[1],DateCreated=new DateTime(2020,4,9),ContentText="Artist Name: Watsky \n Album Name: x Infinity \n Song Title: Talking to Myself \n",             User = User1},
                new Thought{Topic=Topics[2],DateCreated=new DateTime(2020,4,9),ContentText="Add More Seed data to the site \n Establish topic/though crud views \n Prepare for interviews \n",                User = User1},
                new Thought{Topic=Topics[3],DateCreated=new DateTime(2020,4,9),ContentText="1 Entire XL Pepperoni Pizza > 3000 Calories",                User = User1},
                new Thought{Topic=Topics[4],DateCreated=new DateTime(2020,4,9),ContentText="Bike to the beach > 6 Miles",      User = User1},

            };
            foreach (Thought t in Thoughts)
            {
                context.Thoughts.Add(t);
            }
            var Comments = new Comment[]
            {
                new Comment{Thought=Thoughts[0],User=User1,Text="Man what a great entry by me if I dont say so myself"},
                //new Comment{Topic=Topics[0],User=User1,Text="What is the point of this topic?"},
                new Comment{ParentCommentId= new Comment{Topic=Topics[0],User=User1,Text="What is the point of this topic?"},User=User1,Text="What is the point of this Comment?"}
            };
            foreach (Comment c in Comments)
            {
                context.Comments.Add(c);
            }
            context.SaveChanges();
            context.Dispose();
        }

    }

}
