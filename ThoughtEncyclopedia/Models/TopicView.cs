using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    [NotMapped]
    public class TopicView
    {
        public int TopicID { get; set; }
        public int ParentID { get; set; }
        public String Title { get; set; }
        public int KnowlegeTypeID { get; set; } = 0;
        public String Description { get; set; }
        public int  CategoryId { get; set; }
        public List<SelectListItem> Categories { set; get; }


    }
}
