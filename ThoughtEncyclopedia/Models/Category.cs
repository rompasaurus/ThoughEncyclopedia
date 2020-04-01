using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ThoughtEncyclopedia.Models
{
    public enum PrivacyValue
    {
        Private,Friends,Family,FriendAndFamily,Public
    }
    //The Category table is going to dictate the type of Topic and it viewable permisions level that is dictaed by the user
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;

        //The Default of a Category will be set to 0 indiocating that the topic contents will be private
        [DefaultValue(0)]
        public PrivacyValue PrivacyValue { get; set; } = (PrivacyValue)0;
    }
}
