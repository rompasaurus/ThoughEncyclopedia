using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }

        public static explicit operator ApplicationUser(Task<IdentityUser<string>> v)
        {
            throw new NotImplementedException();
        }
    }
}