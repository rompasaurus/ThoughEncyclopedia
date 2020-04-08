using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        //[ConcurrencyCheck]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateModified { get; set; } = DateTime.Now;

        //The Default of a Category will be set to 0 indicating that the topic contents will be private
        [DefaultValue(0)]
        public PrivacyValue PrivacyValue { get; set; } = (PrivacyValue)0;
    }
}
