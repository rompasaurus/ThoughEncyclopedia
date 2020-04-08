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
    //Simplified Poco used for the presentation and submittal of thoughts on website
    [NotMapped]
    public class ThoughtView
    {
        public int ThoughtId { get; set; }
        public string ContentText { get; set; }
        public int TopicId { get; set; }
        public List<SelectListItem> Topics { get; set; }
    }
}
