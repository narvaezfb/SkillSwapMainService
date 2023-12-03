using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models
{
    public class Listing
    {
        public int ListingID { get; set; }

        [Required(ErrorMessage = "Listiing User  is required")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Listingh Skill is required")]
        public int SkillID { get; set; }

        public string? Description { get; set; }

        public DateTime DateAdded { get; set; }

        [Required(ErrorMessage ="Listing location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Listing Availability is required")]
        public string Availability { get; set; }

        public  Skill Skill { get; set; }
    }
}
