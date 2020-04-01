using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    //The Heart of the website is this data model it represents the Users thoughts upon the specified Topic
    public class Thought
    {
        public int ThoughtId { get; set; }
        public string ContentText { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public int ViewCount { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int LikeCount { get; set; }
        public IdentityUser UserId { get; set; }
        public Topic Topic { get; set; }
    }
}
