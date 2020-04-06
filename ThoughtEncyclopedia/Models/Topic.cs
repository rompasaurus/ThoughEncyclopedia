using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    public class Topic
    {
        public int TopicID { get; set; }
        public int ParentID { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [ConcurrencyCheck]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateModified { get; set; } = DateTime.Now;
        public String Title { get; set; }
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int KnowlegeTypeID { get; set; } = 0;
        public String Description { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public Category Category { get; set; }
        public int ViewCount { get; set; } = 0;


    }
}
