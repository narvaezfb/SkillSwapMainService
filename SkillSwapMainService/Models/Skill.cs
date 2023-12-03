using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkillSwapMainService.Models
{
    public class Skill
    {
        public int SkillID { get; set; }

        [Required(ErrorMessage = "Skill name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Skill description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Skill category ID is required")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Skill level is required")]
        public string Level { get; set; }

        [Required(ErrorMessage = "Skill Owner ID is required")]
        public int OwnerID { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsVerified { get; set; }

        [JsonIgnore]
        public ICollection<Listing> Listings { get; set; }

        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> OfferedSkill { get; set; }

        [JsonIgnore]
        public ICollection<Transaction> ReceivedSkill { get; set; }

    }
}
