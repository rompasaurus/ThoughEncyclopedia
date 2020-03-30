using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int ParentCommentId { get; set; }
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        public int LikeCount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public IdentityUser UserID { get; set; }
    }
}
