using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSwapMainService.Models
{
    public class UserSkill
    {
        public int UserSkillID { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Skill ID is required")]
        public int SkillID { get; set; }

        public DateTime DateAdded { get; set; }

        public  Skill Skill { get; set; }
    }
}
