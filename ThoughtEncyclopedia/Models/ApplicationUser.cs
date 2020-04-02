using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThoughtEncyclopedia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
    }
}