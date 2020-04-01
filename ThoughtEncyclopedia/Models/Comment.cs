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
        public int UserId { get; set; }
        public int ParentCommentId { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public IdentityUser UserID { get; set; }
        public Thought Content { get; set; }
    }
}
