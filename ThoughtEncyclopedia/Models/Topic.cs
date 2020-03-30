using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    public class Topic
    {
        public int TopicID { get; set; }
        public Category Category { get; set; }
        public int ParentID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public String Title { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
        public int LikeCount { get; set; }
        public int KnowlegeTypeID { get; set; }
        public String Content { get; set; }
        public IdentityUser UserID { get; set; }
        public int ViewCount { get; set; }


    }
}
