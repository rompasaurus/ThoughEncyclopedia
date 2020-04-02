using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    //Eventual Support for user collaboration and contribution will be set here
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public int UpVoteCount { get; set; } = 0;
        public int DownVoteCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        //indicates who created the comment
        public IdentityUser User { get; set; }
        //whichever these field are not null is what the comment is referencing
        public Comment ParentCommentId { get; set; }
        public Thought Thought { get; set; }
        public Topic Topic { get; set; }
    }
}
